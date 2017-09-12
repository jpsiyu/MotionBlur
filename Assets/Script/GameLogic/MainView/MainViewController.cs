using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainViewController {
    public static void Open() {
        UIManager.Instance.Open<MainView>();
        //MonoBehaviourTool.Instance.MBStartCoroutine(CloseCor());
    }

    public static void Close() {
        UIManager.Instance.Close<MainView>();
    }

    private static IEnumerator CloseCor() {
        yield return new WaitForSeconds(3.0f);
        Close();
        yield return new WaitForSeconds(3.0f);
        UIManager.Instance.Open<MainView>();
        yield return new WaitForSeconds(3.0f);
        Close();

    }

}
