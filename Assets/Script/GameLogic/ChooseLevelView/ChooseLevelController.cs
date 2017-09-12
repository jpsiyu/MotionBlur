using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChooseLevelController {

    public static void Open() {
        UIManager.Instance.Open<ChooseLevelView>();
    }

    public static void Close() {
        UIManager.Instance.Close<ChooseLevelView>();
    }

}
