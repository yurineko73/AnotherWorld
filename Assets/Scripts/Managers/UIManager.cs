using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

    public static UIManager Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public Text levelText;
    public GameObject levelAnimPre; //通关时动画
    public GameObject winParticlesPre; //通关时特效

    void Start()
    {
        levelText.text = MapController.Instance.cur_level + "";
    }

    public void onReloadBtnClick()
    {
        MapController.Instance.reloadMap();
    }
    public void onSaveBtnClick()
    {
        MapController.Instance.saveMapData();
    }
    public void onLeftBtnClick()
    {
        MapController.Instance.InitMap(MapController.Instance.cur_level - 1);
        levelText.text = MapController.Instance.cur_level + "";
    }
    public void onRightBtnClick()
    {
        MapController.Instance.InitMap(MapController.Instance.cur_level + 1);
        levelText.text = MapController.Instance.cur_level + "";
    }
    //胜利播放动画
    public void LevelAnim()
    {
        var levelAnim = Instantiate(levelAnimPre,levelText.transform.parent.parent);
        levelAnim.GetComponentInChildren<Text>().text = (MapController.Instance.cur_level + 1) + "";
        levelAnim.GetComponent<RectTransform>().DOScale(0.8f, 0.5f).SetDelay(0.5f);
        levelAnim.GetComponent<RectTransform>().DOMoveY(1190f, 0.5f).SetDelay(0.5f);
        Tween tween = levelAnim.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetDelay(0.5f);
        tween.onComplete += FinishLoadNext;
        Instantiate(winParticlesPre);
    }
    public void FinishLoadNext()
    {
        MapController.Instance.InitMap(MapController.Instance.cur_level + 1);
        levelText.text = MapController.Instance.cur_level + "";
    }
}
