using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappear : MonoBehaviour {

    private Image image;
    private float offset;
    private float maxY;
    private float minY;
    private float height;
    private float fadeTime = 1f;
    private float fadeTimeCounter = 0f;
    private State mechineState;
    private float distance = 300f;

    private enum State {
        Prepare,
        FadeOut,
        FadeIn,
        FadeEnd,
    }

    private void Awake() {
        image = GetComponent<Image>();
        maxY = image.rectTransform.rect.yMax;
        minY = image.rectTransform.rect.yMin;
    }

    private void Start() {
        StartMechin();
    }

    private void Update() {
        MechinRun();
    }

    private void MechinRun() {
        switch (mechineState) {
            case State.Prepare:
                Prepare();
                break;
            case State.FadeOut:
                FadeOut();
                break;
            case State.FadeIn:
                FadeIn();
                break;
            case State.FadeEnd:
                FadeEnd();
                break;
            default:
                break;
        }
    }

    private void MechineMsg(string msg) {
        if (mechineState == State.Prepare && msg == "prepare_end")
            mechineState = State.FadeOut;
        else if (mechineState == State.FadeOut && msg == "out_end")
            mechineState = State.FadeIn;
        else if (mechineState == State.FadeIn && msg == "in_end")
            mechineState = State.FadeEnd;
    }

    private void StartMechin() {
        mechineState = State.Prepare;
    }

    private void UpdateOffset() {
        image.material.SetFloat("_Offset", offset);
    }

    private void Prepare() {
        MechineMsg("prepare_end");
    }

    private void FadeOut() {
        if (fadeTimeCounter >= fadeTime) {
            transform.localPosition += new Vector3(Random.Range(-distance, distance), Random.Range(-distance, distance), 0);
            fadeTimeCounter = 0;
            MechineMsg("out_end");
            return;
        }
        offset = (1 - fadeTimeCounter / fadeTime) * (maxY - minY);
        offset -= maxY;
        UpdateOffset();
        fadeTimeCounter += Time.deltaTime;

    }
    private void FadeIn() {
        if (fadeTimeCounter >= fadeTime)
            MechineMsg("in_end");

        offset = fadeTimeCounter / fadeTime * (maxY - minY);
        offset -= maxY;
        UpdateOffset();
        fadeTimeCounter += Time.deltaTime;
    }

    private void FadeEnd() { }
}
