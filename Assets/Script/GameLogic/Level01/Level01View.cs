using System.Collections;
using System.Collections.Generic;
using Tangzx.ABSystem;
using UnityEngine;

public class Level01View : ViewBase {

    private GameObject offset;
    private HoriMove horiMove;
    private Transform bornPlace;
    private GameObject player;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        offset = transform.Find("offset").gameObject;
        horiMove = offset.AddComponent<HoriMove>();
        bornPlace = transform.Find("offset/born");
    }

    private void BindEvent() {

    }

    private void Start() {
        horiMove.SetEndAction(delegate { Level01Ctrl.Close(); });
        Run();
    }

    private void Run() {
        Born();
        horiMove.StartMove();
    }

    private void Born() {
        AssetBundleManager.LoadAssetCompleteHandler handler = delegate (AssetBundleInfo abi) {
            player = GameObject.Instantiate(abi.mainObject) as GameObject;
            SimpleComponentUtil.SetParent(bornPlace, player.transform);
        };
        AssetBundleManager.Instance.Load(AssetPathDefinition.player, handler);
    }

    private void UnSafe() { }

    private void Safe() { }

}
