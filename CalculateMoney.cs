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
    private float scoreRate;
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
    }


    private void Start()
    {
        SecondaryInitialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void SecondaryInitialize()
    {
        scoreRate = 1.0f;
        loadRewardMovie.isRewarded.Subscribe(
        isRewarded =>
        {
            if (isRewarded)
            {
                scoreRate = rewardedScoreRate;
            }
            else
            {
                scoreRate = 1.0f;
            }
            Calculate();
        });
        calculatePlayerScore.playerScore.Subscribe(
            _ => Calculate()
            );
    }

    private void Calculate()
    {
        money.Value = (int)(calculatePlayerScore.playerScore.Value * ((1 + (countBonusByItem.bonusPointPerItem / 100)) * scoreRate));
    }
}
