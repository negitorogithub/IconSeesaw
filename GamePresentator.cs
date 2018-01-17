using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using static Assets.Scripts.MyUtil;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class GamePresentator : MonoBehaviour {

    public DataHolder dataHolder;
    public DistanceMeasure distanceMeasure;
    public CountScoreByItem countScoreByItem;
    public Button levelUpButton;
    public Rigidbody2D player;
    public static int money2Add;
    public static ReactiveProperty<int> nextCost;
    public static float score2Add;
    public static int money;
    public static ReactiveProperty<int> playerLevel;
    public static float highScore;
    public static Subject<Unit> retrySender;
    public static bool isPlayerFixed;

    private void Start()
    {
        SeconderyInitialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        PrimaryInitialize();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        PrimaryInitialize();
        SeconderyInitialize();
    }

    private void PrimaryInitialize()
    {
        retrySender = new Subject<Unit>();
        playerLevel = new ReactiveProperty<int>();
        nextCost = new ReactiveProperty<int>();
        levelUpButton?.OnClickAsObservable()
        .ThrottleFirst(TimeSpan.FromSeconds(0.1))
        .Subscribe(
            _ => LevelUpPlayer()
            );
    }
    private void SeconderyInitialize()
    {
        // データホルダの変換を購読
        isPlayerFixed = true;
        countScoreByItem?.scoreByTheItem.Subscribe(
            value => money2Add = (int)(value + Max(score2Add, 0))
            );
        distanceMeasure?.distance.Subscribe(
            value => score2Add = value
            );
        dataHolder?.highScore.Subscribe(
            value => highScore = value
            );
        dataHolder?.playerMoney.Subscribe(
            value => money = value
            );
        dataHolder?.playerLevel.Subscribe(
            value => playerLevel.Value = value
            );
        dataHolder?.playerLevel.Subscribe(
            value => player?.gravityScale.InvokeUtil(_ => player.gravityScale = ((float)value/10))
            );
        dataHolder?.nextCost.Subscribe(
            value => nextCost.Value = value
            );
        
    }

    public void RetryGame()
    {
        retrySender.OnNext(Unit.Default);
        LeanTween.cancelAll(true);
        Pushvalue2DataHolder();
        SceneManager.LoadScene(0);
    }

    public void ChengeScene2Shop()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ChangeScene2Game()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelUpPlayer()
    {
        dataHolder?.playerMoney?.InvokeUtil(
            _ => dataHolder.playerMoney.Value = dataHolder.playerMoney.Value - dataHolder.nextCost.Value);
        dataHolder?.playerLevel?.InvokeUtil(_ => dataHolder.playerLevel.Value++);
        
        Pushvalue2DataHolder();
    }
    private void Pushvalue2DataHolder()
    {
        dataHolder?.InvokeUtil(_ => dataHolder.highScore.Value = Max(dataHolder.highScore.Value, score2Add));
        dataHolder?.InvokeUtil(_ => dataHolder.playerMoney.Value += money2Add);
        dataHolder?.InvokeUtil(_ => dataHolder.playerLevel.Value = playerLevel.Value);
    }

    
}
