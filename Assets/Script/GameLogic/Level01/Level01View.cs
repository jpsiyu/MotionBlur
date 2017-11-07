using System.Collections;
using System.Collections.Generic;
using Tangzx.ABSystem;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Level01View : ViewBase {

    private GameObject offset;
    private HoriMove horiMove;
    private Transform bornPlace;
    private GameObject player;
    private EGameState state;
    private Transform pointRoot;
    private Action resultAction;

    private enum EGameState {
        Initialization,
        WaitForPlayerInput,
        TrainMove,
    }

    private void Awake() {
        state = EGameState.Initialization;
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        offset = transform.Find("offset").gameObject;
        horiMove = offset.AddComponent<HoriMove>();
        bornPlace = transform.Find("offset/born");
        pointRoot = transform.Find("offset/points");
    }

    private void BindEvent() {
        for (int i = 0; i < pointRoot.childCount; i++) {
            GameObject c = pointRoot.GetChild(i).gameObject;
            EventListener.Get(c).onPointerClick = PlayerClick;
        }
    }

    private void Start() {
        horiMove.SetEndAction(GameResult);
        Run();
    }

    private void Run() {
        Born();
        state = EGameState.WaitForPlayerInput;
    }

    private void Born() {
        AssetBundleManager.LoadAssetCompleteHandler handler = delegate (AssetBundleInfo abi) {
            player = GameObject.Instantiate(abi.mainObject) as GameObject;
            SimpleComponentUtil.SetParent(bornPlace, player.transform);
        };
        AssetBundleManager.Instance.Load(AssetPathDefinition.player, handler);
    }

    private void PlayerClick(PointerEventData eventData) {
        GameObject eventObj = eventData.pointerPress;
        player.transform.localPosition = eventObj.transform.localPosition;

        state = EGameState.TrainMove;
        horiMove.StartMove();

        if (eventObj.tag == Tags.safety)
            resultAction = Safe;
        else
            resultAction = UnSafe;
    }

    private void GameResult() {
        resultAction();
    }

    private void UnSafe() {
        Level01Ctrl.Close();
    }

    private void Safe() {
        Level01Ctrl.Close();
    }

}
