using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using static UnityEngine.Mathf;
using System.Collections;
using System;

public class CalculatePlayerScore : MonoBehaviour
{
    
    
    public DistanceMeasure distanceMeasure;
    
    public ReactiveProperty<float> playerScore;
    private float thisDistance;
    
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
        distance =>
        {
            thisDistance = distance;
            Calculate();
        }
        
            );
    }

    private void Calculate()
    {
        playerScore.Value = Max(0, thisDistance);
    }

}
