using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager{
    #region singleton
    private static UIManager _instance;
    public static UIManager Instance {
        get {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }
    private UIManager() { }
    #endregion singleton

    private GameObject uiRootGameObj;
    private Transform layerNormal;
    private Transform layerPopup;

    private Stack<ViewBase> viewStack = new Stack<ViewBase>();
    private void Mount2UILayer(EViewType e, GameObject gameObj) {
        switch (e)
        {
            case EViewType.Normal:
                gameObj.transform.parent = layerNormal;
                break;
            case EViewType.Popup:
                gameObj.transform.parent = layerPopup;
                break; ;
            default:
                break;
        }
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.transform.localScale = Vector3.one;
    }

    private ViewBase Contains(System.Type t) {
        foreach (ViewBase view in viewStack) {
            if (view.GetType() == t)
                return view;
        }
        return null;
    }

    public GameObject UIRootGameObj {
        set {
            if (uiRootGameObj == null) {
                uiRootGameObj = value;
                layerNormal = uiRootGameObj.transform.Find("UILayers/LayerNormal");
                layerPopup = uiRootGameObj.transform.Find("UILayers/LayerPopup");
            }
        }
    }

    public void Open<T>() where T: ViewBase{
        System.Type t = typeof(T);
        ViewBase view = Contains(t);

        if (view != null){
            view.gameObject.SetActive(true);
        }
        else {
            ViewPathStr str = ViewPathDefinition.GetStr(t);
            if (str.IsNull()) {
                throw new MissingReferenceException("view path is null");
            }
            Object prefab = Resources.Load(str.path);
            GameObject gameObj = GameObject.Instantiate(prefab) as GameObject;
            Mount2UILayer(str.eViewType, gameObj);
            view = gameObj.AddComponent<T>();
            viewStack.Push(view);
        }
    }

    public void Close(ViewBase view) { }
}
