using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level01Ctrl  {
    public static void Open() {
        UIManager.Instance.Open<Level01View>();
    }

    public static void Close() {
        UIManager.Instance.Close<Level01View>();
    }

}
