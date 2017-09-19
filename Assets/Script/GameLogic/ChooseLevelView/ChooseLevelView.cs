using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelView : ViewBase {
    private Text textClose;
    private Transform content;
    private GameObject item;

    private void Awake() {
        BindUI();
        BindEvent();
    }

    private IEnumerator Start() {
        List<LevelInfo> levelInfo = ChooseLevelController.GetLevelInfo();
        for (int i = 0; i < levelInfo.Count; i++) {
            GenLevelItem(levelInfo[i]);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private GameObject GenLevelItem(LevelInfo lvInfo) {
        GameObject gameObj = GameObject.Instantiate(item);
        SimpleComponentUtil.SetParent(content, gameObj.transform);
        Text texComp = gameObj.transform.Find("Image/Text").GetComponent<Text>();
        texComp.text = lvInfo.id.ToString();
        GameObject imgGameObj = gameObj.transform.Find("Image").gameObject;
        EventListener.Get(imgGameObj).onPointerClick = delegate { lvInfo.openLvView(); } ;
        gameObj.SetActive(true);
        return gameObj;
    }

    private void BindUI() {
        textClose = transform.Find("TextClose").GetComponent<Text>();
        content = transform.Find("Scroll View/Viewport/Content");
        item = transform.Find("Scroll View/Item").gameObject;
    }
    private void BindEvent() {
        EventListener.Get(textClose.gameObject).onPointerClick = OnTextCloseClick;
    }

    private void OnTextCloseClick() {
        ChooseLevelController.Close();
    }
}
