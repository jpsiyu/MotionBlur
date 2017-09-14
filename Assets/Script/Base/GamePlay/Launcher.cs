using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tangzx.ABSystem;

public class Launcher : MonoBehaviour {

    private void Start() {
        BoostABMgr();
    }

    private void BoostABMgr() {
        gameObject.AddComponent<AssetBundleManager>();
        AssetBundleManager.Instance.Init(BoostAfterABMgr);
    }

    private void BoostAfterABMgr() {
        BoostUI();

    }

    private void BoostAfterUI() {
        BoostTools();
        BoostFinish();
    }

    private void BoostUI() {
        AssetBundleManager.LoadAssetCompleteHandler handler = delegate (AssetBundleInfo abi) {
            GameObject gameObj = GameObject.Instantiate(abi.mainObject) as GameObject;
            DontDestroyOnLoad(gameObj);
            UIManager.Instance.UIRootGameObj = gameObj;
            ViewPropertyDefinition.Init();
            BoostAfterUI();
        };
        AssetBundleManager.Instance.Load("Assets.GameResources.Prefab.UIRoot.prefab", handler);
    }

    private void BoostTools() {
        gameObject.AddComponent<MonoBehaviourTool>();
    }

    private void BoostFinish() {
        GameObject.Destroy(this);
        MainViewController.Open();
    }
}
