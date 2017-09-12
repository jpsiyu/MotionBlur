using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventListener : EventTrigger {

    public delegate void EventDelegate();
    public EventDelegate onPointerClick;
    public EventDelegate onPointerDown;
    public EventDelegate onPointerEnter;
    public EventDelegate onPointerExit;
    public EventDelegate onPointerUp;
    public EventDelegate onSelect;
    public EventDelegate onUpdateSelected;

    public static EventListener Get(GameObject gameObj) {
        EventListener listener = gameObj.GetComponent<EventListener>();
        if (listener == null)
            listener = gameObj.AddComponent<EventListener>();
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData) {
        if (onPointerClick != null)
            onPointerClick();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        if (onPointerDown != null)
            onPointerDown();
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if (onPointerEnter != null)
            onPointerEnter();
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (onPointerExit != null)
            onPointerExit();
    }

    public override void OnPointerUp(PointerEventData eventData) {
        if (onPointerUp != null)
            onPointerUp();
    }

    public override void OnSelect(BaseEventData eventData) {
        if (onSelect != null)
            onSelect();
    }

    public override void OnUpdateSelected(BaseEventData eventData) {
        if (onUpdateSelected != null)
            onUpdateSelected();
    }
}
