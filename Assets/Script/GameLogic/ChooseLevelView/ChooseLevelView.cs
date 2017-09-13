﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelView : ViewBase {
    private Text textClose;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        textClose = transform.Find("TextClose").GetComponent<Text>();
    }
    private void BindEvent() {
        EventListener.Get(textClose.gameObject).onPointerClick = OnTextCloseClick;
    }

    private void OnTextCloseClick() {
        ChooseLevelController.Close();
    }
}
