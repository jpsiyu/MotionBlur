using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ViewPropertySt {
    public System.Type classType;
    public string path;
    public EViewType eViewType;
    public ViewPropertySt(System.Type t, string path, EViewType e) {
        this.classType = t;
        this.path = path;
        this.eViewType = e;
    }

    public bool IsNull() {
        return classType == null;
    }
}

public static class ViewPropertyDefinition {

    private static Dictionary<System.Type, ViewPropertySt> dict;

    public static void Init() {
        dict = new Dictionary<System.Type, ViewPropertySt>();
        dict.Add(typeof(MainView), new ViewPropertySt(typeof(MainView), "Prefab/MainView", EViewType.Normal));
        dict.Add(typeof(ChooseLevelView), new ViewPropertySt(typeof(ChooseLevelView), "Prefab/ChooseLevelView", EViewType.Normal));
    }

    public static ViewPropertySt GetSt(System.Type t) {
        ViewPropertySt st;
        dict.TryGetValue(t, out st);
        return st;
    }
}
