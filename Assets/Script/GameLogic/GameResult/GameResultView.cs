using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultView : ViewBase {

    private Transform win;
    private Transform lose;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        win = transform.Find("Win");
        lose = transform.Find("Lose");
    }

    private void BindEvent() {
    }

    private void Start() {
        bool isWin = GameResultModel.Instance.Result == EGameResult.Win;
        win.gameObject.SetActive(isWin);
        lose.gameObject.SetActive(!isWin);
    }
}
