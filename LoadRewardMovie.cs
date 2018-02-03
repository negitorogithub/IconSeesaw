using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UniRx;
using System.Collections;
using System;

public class LoadRewardMovie : MonoBehaviour
{
    public ReactiveProperty<bool> isRewarded;
    public Subject<Unit> onVideoCompletedSubject;
    public RewardBasedVideoAd rewardBasedVideo;
    private string adUnitId;
    private AdRequest adRequest;
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        PrimaryInitialize();
        SecondaryInitialize();
    }

    private void Awake()
    {
        PrimaryInitialize();
    }

    private void Start()
    {
        SecondaryInitialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void PrimaryInitialize()
    {
        isRewarded = new ReactiveProperty<bool>();
        onVideoCompletedSubject = new Subject<Unit>();
    }

    private void SecondaryInitialize()
    {
        
        LoadAd();
    }
    private void RequestRewardedVideo()
    {
        adUnitId = "ca-app-pub-6418178360564076/1346262531";
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
    }

    public void LoadAd()
    {
        RequestRewardedVideo();
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //RewardVideo準備完了
        onVideoCompletedSubject.OnNext(Unit.Default);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        isRewarded.Value = true;
        
    }
}
