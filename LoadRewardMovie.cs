using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UniRx;
using System.Collections;
using System;

public class LoadRewardMovie : MonoBehaviour
{
    public Subject<Unit> onVideoCompletedSubject;
    public RewardBasedVideoAd rewardBasedVideo;
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Initialize()
    {
        onVideoCompletedSubject = new Subject<Unit>();
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;

    }
    private void RequestRewardedVideo()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //RewardVideo準備完了
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);

    }
}
