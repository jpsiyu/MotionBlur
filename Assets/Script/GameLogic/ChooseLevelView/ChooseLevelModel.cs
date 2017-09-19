using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChooseLevelModel{
    private static ChooseLevelModel _instance;
    private List<LevelInfo> levelInfoList;

    public static ChooseLevelModel Instance {
        get {
            if (_instance == null)
                _instance = new ChooseLevelModel();
            return _instance;
        }
    }

    public List<LevelInfo> GetLevelInfo() {
        return levelInfoList;
    }

    private ChooseLevelModel() {
        levelInfoList = new List<LevelInfo>();
        InitLevelInfo();
    }

    private void InitLevelInfo() {
        for (int i = 0; i < 15; i++) {
            LevelInfo li = new LevelInfo(i, delegate { UIManager.Instance.Open<TipsView>(); });
            levelInfoList.Add(li);
        }
    }
}

public class LevelInfo {
    public int id;
    public Action openLvView;

    public LevelInfo(int id, Action openLvView) {
        this.id = id;
        this.openLvView = openLvView;
    }
}