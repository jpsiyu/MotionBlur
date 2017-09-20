using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01View : ViewBase {

    private GameObject offset;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        offset = transform.Find("offset").gameObject;
        offset.AddComponent<HoriMove>();
    }

    private void BindEvent() {

    }

}
