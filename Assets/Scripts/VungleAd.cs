using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VungleAd : MonoBehaviour
{

    public string appId;
    
    void Start()
    {
        Debug.Log("vungle init");
        //广告实例化，一次即可。
        Vungle.init(appId);
        initializeEventHandlers();
    }

    void Update()
    {

    }
    //广告对应事件。
    void initializeEventHandlers()
    {
        Vungle.adPlayableEvent += (placementID, adPlayable) =>
        {
            if (placementID == "DEFAULT-7494164")
            {
                //判断广告是否加载完成
            }
        };
        Vungle.onAdStartedEvent += (placementID) =>
        {
            Debug.Log("Ad " + placementID + " is starting!  Pause your game  animation or sound here.");
        };
        Vungle.onAdFinishedEvent += (placementID, args) =>
        {
            Debug.Log("Ad finished - placementID " + placementID + ", was call to action clicked:" + args.WasCallToActionClicked + ", is completed view:"
                + args.IsCompletedView);
            if (args.IsCompletedView)
            {
                //完成观看，奖励逻辑
            }
        };
        Vungle.adPlayableEvent += (placementID, adPlayable) =>
        {
            Debug.Log("Ad's playable state has been changed! placementID " + placementID + ". Now: " + adPlayable);
            //placements[placementID] = adPlayable;
        };
        Vungle.onLogEvent += (log) =>
        {
            Debug.Log("Log: " + log);
        };
        Vungle.onInitializeEvent += () =>
        {
            Debug.Log("SDK initialized");
            LoadRewardAd("DEFAULT-7494164");
        };
    }
    //加载方法adUnitId是填写对应的"VunglePlacementID"
    public void LoadRewardAd(string adUnitId)
    {
        Vungle.loadAd(adUnitId);
    }
    //显示激励广告
    public void ShowRewardAd(string adUnitId)
    {
        //Vungle.isAdvertAvailable(adUnitId)判断对应ID广告是否可显示。
        if (Vungle.isAdvertAvailable(adUnitId))
        {
            Vungle.playAd(adUnitId);
        }

    }
}
