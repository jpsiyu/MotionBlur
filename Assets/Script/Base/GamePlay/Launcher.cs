using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    private void Start() {
        BoostUI();
        BoostTools();
        BoostFinish();
    }

    private void BoostUI() {
        Object prefab = Resources.Load("Prefab/UIRoot");
        GameObject gameObj = GameObject.Instantiate(prefab) as GameObject;
        DontDestroyOnLoad(gameObj);
        UIManager.Instance.UIRootGameObj = gameObj;

        ViewPathDefinition.Init();
    }

    private void BoostTools() {
        gameObject.AddComponent<MonoBehaviourTool>();
    }

    private void BoostFinish() {
        GameObject.Destroy(this);
        MainViewController.Open();
    }
}
