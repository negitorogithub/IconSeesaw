using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;
using System;

public class CalculatePlayerScore : MonoBehaviour
{

    public DistanceMeasure distanceMeasure;
    public CountBonusByItem countBonusByItem;
    public ReactiveProperty<float> playerScore;
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        PrimaryInitialize();
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
        playerScore = new ReactiveProperty<float>();

    }

    private void SecondaryInitialize()
    {
        distanceMeasure.distance.Subscribe(
        distance => playerScore.Value = distance * (1 + (countBonusByItem.bonusPointPerItem / 100))
        );
    }

}
