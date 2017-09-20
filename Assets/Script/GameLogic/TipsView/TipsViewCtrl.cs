using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TipsViewCtrl {

    public static void Open() {
        UIManager.Instance.Open<TipsView>();
    }

    public static void Close() {
        UIManager.Instance.Close<TipsView>();
    }

}
