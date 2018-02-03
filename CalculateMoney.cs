using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;
using System;

public class CalculateMoney : MonoBehaviour
{
    public ReactiveProperty<int> money;
    public CalculatePlayerScore calculatePlayerScore;
    public LoadRewardMovie loadRewardMovie;
    public CountBonusByItem countBonusByItem;
    [SerializeField]
    private float rewardedScoreRate;// > 1.0f
    public ReactiveProperty<float> scoreRateSum { get; private set; }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        SecondaryInitialize();
    }

    private void Awake()
    {
        PrimaryInitialize();
    }

    private void PrimaryInitialize()
    {
        money = new ReactiveProperty<int>();
        scoreRateSum = new ReactiveProperty<float>();
    }


    private void Start()
    {
        SecondaryInitialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void SecondaryInitialize()
    {
        float scoreRateByReward = 1.0f;
        loadRewardMovie.isRewarded.Subscribe(
        isRewarded =>
        {
            if (isRewarded)
            {
                scoreRateByReward = rewardedScoreRate;
            }
            else
            {
                scoreRateByReward = 1.0f;
            }
            Calculate(scoreRateByReward);
        });
        calculatePlayerScore.playerScore.Subscribe(
            _ => Calculate(scoreRateByReward)
            );
    }

    private void Calculate(float bonusScoreRate)
    {
        scoreRateSum.Value = (1 + (countBonusByItem.bonusPointByTheItem.Value / 100)) * bonusScoreRate;
        money.Value = (int)(calculatePlayerScore.playerScore.Value * scoreRateSum.Value);
    }
}
