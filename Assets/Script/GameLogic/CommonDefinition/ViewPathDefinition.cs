using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ViewPathStr {
    public System.Type classType;
    public string path;
    public EViewType eViewType;
    public ViewPathStr(System.Type t, string path, EViewType e) {
        this.classType = t;
        this.path = path;
        this.eViewType = e;
    }

    public bool IsNull() {
        return classType == null;
    }
}

public static class ViewPathDefinition{

    private static Dictionary<System.Type, ViewPathStr> dict;

    public static void Init() {
        dict = new Dictionary<System.Type, ViewPathStr>();
        dict.Add(typeof(MainView), new ViewPathStr(typeof(MainView), "Prefab/MainView", EViewType.Normal));
    }

    public static ViewPathStr GetStr(System.Type t) {
        ViewPathStr str;
        dict.TryGetValue(t, out str);
        return str;
    }
}
