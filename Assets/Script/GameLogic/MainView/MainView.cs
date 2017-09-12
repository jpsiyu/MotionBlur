using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : NormalView {
    private Image img;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        img = transform.Find("Image").GetComponent<Image>();
    }
    private void BindEvent() {
        EventListener.Get(img.gameObject).onPointerClick = OnImgClick;
    }

    private void OnImgClick() {
        ChooseLevelController.Open();
    }
}
