using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SimpleComponentUtil{

    public static Text AddComponentText(Transform parent, string text) {
        GameObject gameObj = new GameObject();
        SetParent(parent, gameObj.transform);
        Text t = gameObj.AddComponent<Text>();
        t.text = text;
        return t;
    }

    public static Image AddComponentImg(Transform parent) {
        GameObject gameObj = new GameObject();
        SetParent(parent, gameObj.transform);
        return gameObj.AddComponent<Image>();
    }

    public static void SetParent(Transform parent, Transform child) {
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
    }
}
