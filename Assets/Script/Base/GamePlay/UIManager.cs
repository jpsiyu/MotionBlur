using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tangzx.ABSystem;

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
    private GameObject alphaMask;
    private Camera uiCamera;

    private Stack<ViewBase> viewStack = new Stack<ViewBase>();
    private void Mount2UILayer(EViewType e, GameObject gameObj) {
        Transform parent = e == EViewType.Normal ? layerNormal : layerPopup;
        gameObj.transform.SetParent(parent);
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.transform.localScale = Vector3.one;

        if (e == EViewType.Normal)
            AnchorMax(gameObj);
    }

    private void AnchorMax(GameObject gameObj) {
        RectTransform rect = gameObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private ViewBase Contains(System.Type t) {
        foreach (ViewBase view in viewStack) {
            if (view.GetType() == t)
                return view;
        }
        return null;
    }

    private void AfterPush(EViewType e) {
        if (e == EViewType.Normal) {
            foreach (ViewBase view in viewStack) {
                if (view.gameObject.activeInHierarchy)
                    view.gameObject.SetActive(false);
            }
        }
        else if (e == EViewType.Popup) {
            bool meetNormal = false;
            foreach (ViewBase view in viewStack) {
                if (meetNormal && view.gameObject.activeInHierarchy)
                    view.gameObject.SetActive(false);
                else if (!view.gameObject.activeInHierarchy)
                    view.gameObject.SetActive(true);

                ViewPropertySt st = ViewPropertyDefinition .GetSt(view.GetType());
                if (st.eViewType == EViewType.Normal)
                    meetNormal = true;
            }
        }
    }

    private void AfterPop(EViewType e) {
        if (e == EViewType.Normal) {
            bool meetNormal = false;
            foreach (ViewBase view in viewStack) {
                if (!meetNormal && !view.gameObject.activeInHierarchy)
                    view.gameObject.SetActive(true);

                ViewPropertySt st = ViewPropertyDefinition .GetSt(view.GetType());
                if (st.eViewType == EViewType.Normal)
                    meetNormal = true;
            }
        }
    }

    private void IfUseMask() {
        if (viewStack.Count <= 0)
            return;
        ViewBase v = viewStack.Peek();
        ViewPropertySt str = ViewPropertyDefinition.GetSt(v.GetType());
        EViewType e = str.eViewType;
        if (e == EViewType.Popup && !alphaMask.activeInHierarchy)
            alphaMask.SetActive(true);
        else if (e == EViewType.Normal && alphaMask.activeInHierarchy)
            alphaMask.SetActive(false);
    }

    private void AddCameraMotionBlur() {
        MotionBlur mb = uiCamera.gameObject.AddComponent<MotionBlur>();
        mb.motionBlurShader = Shader.Find("Custom/Motion Blur");
        mb.blurAmount = 0.9f;
        EventManager.Instance.Send(new BlurSwitchEvent("close"));
    }

    public GameObject UIRootGameObj {
        set {
            if (uiRootGameObj == null) {
                uiRootGameObj = value;
                layerNormal = uiRootGameObj.transform.Find("UILayers/LayerNormal");
                layerPopup = uiRootGameObj.transform.Find("UILayers/LayerPopup");
                alphaMask = uiRootGameObj.transform.Find("UILayers/LayerPopup/AlphaMask").gameObject;
                uiCamera = uiRootGameObj.transform.Find("UICamera").GetComponent<Camera>();
                AddCameraMotionBlur();
            }
        }
    }

    public void Open<T>() where T: ViewBase{
        System.Type t = typeof(T);
        ViewBase view = Contains(t);
        ViewPropertySt str = ViewPropertyDefinition .GetSt(t);
        AfterPush(str.eViewType);


        if (view != null){
            view.gameObject.SetActive(true);
            viewStack.Push(view);
        }
        else {
            if (str.IsNull()) {
                throw new MissingReferenceException("view path is null");
            }
            AssetBundleManager.Instance.Load(str.path, (abi) => {
                GameObject gameObj = GameObject.Instantiate(abi.mainObject) as GameObject;
                Mount2UILayer(str.eViewType, gameObj);
                view = gameObj.AddComponent<T>();
                viewStack.Push(view);
                IfUseMask();
            });
        }
    }

    public void Close<T>() where T : ViewBase {
        System.Type t = typeof(T);
        ViewBase view = viewStack.Peek();
        if (view == null){
            throw new System.ArgumentException("not a view exit");
        }
        else if (view.GetType() != t) {
            throw new System.ArgumentException("close a view not at top");
        }
        else {
            viewStack.Pop();
            GameObject.Destroy(view.gameObject);
            ViewPropertySt str = ViewPropertyDefinition .GetSt(t);
            AfterPop(str.eViewType);
            IfUseMask();
        }
    }
}
