using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            LevelInfo li = new LevelInfo(i);
            levelInfoList.Add(li);
        }
    }
}

public class LevelInfo {
    public int id;

    public LevelInfo(int id) {
        this.id = id;
    }
}