using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultModel {
    private static GameResultModel _instance;
    private EGameResult result;

    public static GameResultModel Instance {
        get {
            if (_instance == null)
                _instance = new GameResultModel();
            return _instance;
        }
    }

    public EGameResult Result {
        get { return result; }
        set { result = value; }
    }
}
