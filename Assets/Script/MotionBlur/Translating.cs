using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translating : MonoBehaviour {

    public float distance = 3.0f;
    private float interval = 0.3f;
    private float timeRecord = 0f;
    private int r;

	// Use this for initialization
	void Start () {
        timeRecord = 0f;
        r = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update () {
        if (timeRecord >= interval) {
            Back();
            timeRecord = 0f;
            r = Random.Range(0, 4);
        }
        Move(r);
        timeRecord += Time.deltaTime;
    }

    private void Move(int r) {
        if (r == 0)
            ToUp();
        else if (r == 1)
            ToDown();
        else if (r == 2)
            ToLeft();
        else
            ToRight();
    }

    private void ToRight() {
        transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.right, interval);
    }

    private void ToLeft() {
        transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.left, interval);
    }

    private void ToUp() {
        transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up, interval);
    }

    private void ToDown() {
        transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.down, interval);
    }

    private void Back() {
        transform.localPosition = Vector3.zero;
    }
}
