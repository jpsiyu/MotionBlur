using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01View : ViewBase {

    private GameObject offset;
    private HoriMove horiMove;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        offset = transform.Find("offset").gameObject;
        horiMove = offset.AddComponent<HoriMove>();
    }

    private void BindEvent() {

    }

    private void Start() {
        horiMove.SetEndAction(delegate { Level01Ctrl.Close(); });
        horiMove.StartMove();
    }

}
