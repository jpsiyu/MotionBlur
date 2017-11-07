using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResultCtrl  {
    public static void Open() {
        UIManager.Instance.Open<GameResultView>();
    }

    public static void Close() {
        UIManager.Instance.Close<GameResultView>();
    }
}
