using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChooseLevelModel{
    private int levelNum;
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
        levelNum = 0;
        levelInfoList = new List<LevelInfo>();
        InitLevelInfo();
    }

    private void InitLevelInfo() {
        levelInfoList.Add(new LevelInfo(LvTakePlace(), delegate { UIManager.Instance.Open<Level01View>(); }));

        for (int i = 0; i < 15; i++) {
            levelInfoList.Add(new LevelInfo(LvTakePlace(), delegate { UIManager.Instance.Open<TipsView>(); }));
        }
    }

    private int LvTakePlace() {
        return levelNum++;
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