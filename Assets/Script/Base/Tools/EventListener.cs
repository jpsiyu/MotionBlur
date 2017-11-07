using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventListener : EventTrigger {

    public delegate void EventDelegate(PointerEventData eventData);
    public delegate void BaseEventDelegate(BaseEventData eventData);

    public EventDelegate onPointerClick;
    public EventDelegate onPointerDown;
    public EventDelegate onPointerEnter;
    public EventDelegate onPointerExit;
    public EventDelegate onPointerUp;
    public BaseEventDelegate onSelect;
    public BaseEventDelegate onUpdateSelected;

    public static EventListener Get(GameObject gameObj) {
        EventListener listener = gameObj.GetComponent<EventListener>();
        if (listener == null)
            listener = gameObj.AddComponent<EventListener>();
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData) {
        if (onPointerClick != null)
            onPointerClick(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData) {
        if (onPointerDown != null)
            onPointerDown(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if (onPointerEnter != null)
            onPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (onPointerExit != null)
            onPointerExit(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        if (onPointerUp != null)
            onPointerUp(eventData);
    }

    public override void OnSelect(BaseEventData eventData) {
        if (onSelect != null)
            onSelect(eventData);
    }

    public override void OnUpdateSelected(BaseEventData eventData) {
        if (onUpdateSelected != null)
            onUpdateSelected(eventData);
    }
}
