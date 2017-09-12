using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainViewController {
    public static void Open() {
        UIManager.Instance.Open<MainView>();
    }

}
