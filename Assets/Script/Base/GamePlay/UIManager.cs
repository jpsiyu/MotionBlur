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
    public GameObject UIRootGameObj {
        set {
            if (uiRootGameObj != null) {
                uiRootGameObj = value;
            }
        }
    }

    private Stack<IViewBase> viewStack = new Stack<IViewBase>();

}
