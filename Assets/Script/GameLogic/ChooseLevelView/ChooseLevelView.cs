using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelView : ViewBase {
    private Image img;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        img = transform.Find("ImageClose").GetComponent<Image>();
    }
    private void BindEvent() {
        EventListener.Get(img.gameObject).onPointerClick = OnImgClick;
    }

    private void OnImgClick() {
        ChooseLevelController.Close();
    }
}
