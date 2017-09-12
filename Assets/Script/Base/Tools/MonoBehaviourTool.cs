using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourTool : MonoBehaviour {
    private static MonoBehaviourTool _instance;
    public static MonoBehaviourTool Instance {
        get { return _instance; }
    }

    private void Awake() {
        _instance = this;
    }

    public void MBStartCoroutine(IEnumerator cor) {
        StartCoroutine(cor);
    }
}
