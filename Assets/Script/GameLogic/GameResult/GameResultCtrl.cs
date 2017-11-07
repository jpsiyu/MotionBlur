using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResultCtrl {

    public static void Win() {
        GameResultModel.Instance.Result = EGameResult.Win;
        UIManager.Instance.Open<GameResultView>();
    }
    public static void Lose() {
        GameResultModel.Instance.Result = EGameResult.Lose;
        UIManager.Instance.Open<GameResultView>();
    }

    public static void Close() {
        UIManager.Instance.Close<GameResultView>();
    }
}
