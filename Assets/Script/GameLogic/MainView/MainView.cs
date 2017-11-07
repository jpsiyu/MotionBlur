using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainView : NormalView {
    private Image img;
    private Image img2;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private void BindUI() {
        img = transform.Find("Image").GetComponent<Image>();
        img2 = transform.Find("Image2").GetComponent<Image>();
    }
    private void BindEvent() {
        EventListener.Get(img.gameObject).onPointerClick = OnImgClick;
        EventListener.Get(img2.gameObject).onPointerClick = OnImgClick2;
    }

    private void OnImgClick(PointerEventData eventData) {
        ChooseLevelCtrl.Open();
    }

    private void OnImgClick2(PointerEventData eventData) {
        TipsViewCtrl.Open();
    }
}
