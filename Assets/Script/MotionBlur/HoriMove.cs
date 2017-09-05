using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoriMove : MonoBehaviour {
    private MechineState ms;
    private float prepareWait = 1f;
    private float prepareWaitCounter = 0f;
    private float chaosTime = 0.2f;
    private float chaosTimeCounter = 0f;
    private int chaosRound = 5;
    private int chaosRoundCounter = 0;
    private Vector3 chaosDir = Vector3.zero;
    private float horiMoveTime = 1f;
    private float horiMoveTimeCounter = 0f;
    private float chaosDistance = 40f;
    private Vector3 temp = Vector3.zero;
    private float backTime = 2f;
    private float backTimeCounter = 0f;
    private bool sendClose = false;

    private enum MechineState {
        Prepare,
        Chaos,
        Horizontal,
        Back2Origin,
        End,
    }

    protected void Start()
    {
        ms = MechineState.Prepare;
        EventManager.Instance.Send(new BlurSwitchEvent("open"));
    }

    protected void Update()
    {
        MechineRun();
    }

    protected void MechineRun() {
        switch (ms) {
            case MechineState.Prepare:
                Prepare();
                break;
            case MechineState.Chaos:
                ChaosMove();
                break;
            case MechineState.Horizontal:
                HorizontalMove();
                break;
            case MechineState.Back2Origin:
                BackToOrigin();
                break;
            case MechineState.End:
                EndMove();
                break;
            default:
                EndMove();
                break;
        }
    }

    protected void MechineMsg(string msg) {
        if (ms == MechineState.Prepare && msg == "prepare_finish")
            ms = MechineState.Chaos;
        else if (ms == MechineState.Chaos && msg == "chaos_end")
            ms = MechineState.Horizontal;
        else if (ms == MechineState.Horizontal && msg == "hori_end")
            ms = MechineState.Back2Origin;
        else if (ms == MechineState.Back2Origin && msg == "back_end")
            ms = MechineState.End;
    }

    protected void Prepare() {
        if (prepareWaitCounter > prepareWait)
            MechineMsg("prepare_finish");
        prepareWaitCounter += Time.deltaTime;
    }

    protected void ChaosMove() {
        if (chaosDir == Vector3.zero)
            chaosDir = RandomDir();

        if (chaosTimeCounter >= chaosTime) {
            chaosTimeCounter = 0;
            chaosRoundCounter += 1;
            transform.localPosition = Vector3.zero;
            chaosDir = RandomDir();
        }

        if (chaosRoundCounter >= chaosRound) {
            MechineMsg("chaos_end");
        }

        transform.localPosition = Vector3.Lerp(Vector3.zero, chaosDir, chaosTimeCounter/chaosTime);
        chaosTimeCounter += Time.deltaTime;
    }

    protected void HorizontalMove() {
        if (horiMoveTimeCounter >= horiMoveTime)
            MechineMsg("hori_end");

        transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(Screen.width / 4, 0, 0), horiMoveTimeCounter/horiMoveTime);
        horiMoveTimeCounter += Time.deltaTime;
    }

    protected void BackToOrigin() {
        if (!sendClose) {
            EventManager.Instance.Send(new BlurSwitchEvent("close"));
            sendClose = true;
        }

        if (temp == Vector3.zero)
            temp = transform.localPosition;
        if (backTimeCounter >= backTime)
            MechineMsg("back_end");

        transform.localPosition = Vector3.Lerp(temp, Vector3.zero, backTimeCounter/backTime);
        backTimeCounter += Time.deltaTime;
    }

    protected void EndMove() {

    }

    private Vector3 RandomDir() {
        float h = Random.Range(1, chaosDistance);
        float v = Random.Range(1, chaosDistance);
        float hd = Random.Range(1, 10) > 5 ? 1 : -1;
        float vd = Random.Range(1, 10) > 5 ? 1 : -1;
        Vector3 result = new Vector3(h*hd, v*vd, 0);
        return result;
    }
}
