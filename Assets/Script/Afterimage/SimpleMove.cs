using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

    private float moveTime = 3f;
    private float moveTimeCounter = 0f;
    private void Update() {
        if (moveTimeCounter >= moveTime)
            return;

        transform.position = Vector3.Lerp(Vector3.zero, new Vector3(8, 0, 0), moveTimeCounter/ moveTime);
        moveTimeCounter += Time.deltaTime;
    }
}
