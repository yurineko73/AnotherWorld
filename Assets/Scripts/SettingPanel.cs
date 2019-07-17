using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SettingPanel : MonoBehaviour {

    public void onAdBtnClick()
    {

    }
    public void onSoundBtnClick()
    {

    }
    public void onTipBtnClick()
    {

    }
    public void onSettingBtnClick()
    {
        ShowOrHide();
    }
    public void onSelfBtnClick()
    {
        ShowOrHide(false);
    }
    private void ShowOrHide(bool bshow = true)
    {
        GetComponent<CanvasGroup>().alpha = bshow?1:0;
        GetComponent<CanvasGroup>().blocksRaycasts = bshow;
        if (!bshow) return;
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.zero;
            transform.GetChild(i).GetComponent<RectTransform>().DOScale(0.6f, 0.2f).SetDelay(0.1f * i);
        }
    }
	
}
