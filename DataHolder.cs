using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using UniRx;
using static UnityEngine.Mathf;
using static Assets.Scripts.MyUtil;
using System.Collections;


public class DataHolder : MonoBehaviour
{

    public ReactiveProperty<int> playerLevel;
    public ReactiveProperty<int> playerMoney;
    public ReactiveProperty<float> highScore;
    public ReactiveProperty<int> nextCost;

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        PrimaryInitialize();
        SecondaryInitialize();
    }

    private void Start()
    {
        SecondaryInitialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        PrimaryInitialize();
    }

    private void PrimaryInitialize()
    {

        nextCost = new ReactiveProperty<int>();

        playerMoney = new ReactiveProperty<int>
        {
            Value = PlayerPrefsIO.GetInt(PLAYER_PREFS_KEY_ENUM.MONEY_KEY)
        };
        playerMoney.Subscribe(
        _ => PlayerPrefsIO.SaveInt(PLAYER_PREFS_KEY_ENUM.MONEY_KEY, playerMoney.Value)
            );

        playerLevel = new ReactiveProperty<int>
        {
            Value = PlayerPrefsIO.GetInt(PLAYER_PREFS_KEY_ENUM.LEVEL_KEY)
        };
        playerLevel.Subscribe(
            level => PlayerPrefsIO.SaveInt(PLAYER_PREFS_KEY_ENUM.LEVEL_KEY, level)

            );
        highScore = new ReactiveProperty<float>
        {
            Value = PlayerPrefsIO.GetFloat(PLAYER_PREFS_KEY_ENUM.HIGHSCORE_KEY)
        };
        highScore.Subscribe(
            _ => PlayerPrefsIO.SaveFloat(PLAYER_PREFS_KEY_ENUM.HIGHSCORE_KEY, highScore.Value)
            );
        
    }
    private void SecondaryInitialize()
    {
        playerLevel.Subscribe(
            level => SetNextCost(level)
            );
        Debug.Log("NextCost =" +nextCost.Value);
    }

    public void saveAllData2PlayerPrefs()
    {
        PlayerPrefsIO.SaveInt(PLAYER_PREFS_KEY_ENUM.LEVEL_KEY, playerLevel.Value);
        PlayerPrefsIO.SaveInt(PLAYER_PREFS_KEY_ENUM.MONEY_KEY, playerMoney.Value);
        PlayerPrefsIO.SaveFloat(PLAYER_PREFS_KEY_ENUM.HIGHSCORE_KEY, highScore.Value);
        Debug.Log("HighScore="+PlayerPrefs.GetFloat(PLAYER_PREFS_KEY_ENUM.HIGHSCORE_KEY.ToString()));
    }
    
    private void SetNextCost(int level)
    {
        int processedNextCost = -1;
        if (LoadMasterData.masterData == null)
        {
            return;
        }
        else
        {
            int[] levelUpCosts = LoadMasterData.masterData.costs2LevelUp;
            int lastCostsIndex = levelUpCosts.Length -1;
            //                               ↓Lv.0時の対策
            processedNextCost = levelUpCosts[Max(level - 1, 0)];
            if (lastCostsIndex < Max(level - 1, 0))
            {
                processedNextCost = levelUpCosts[lastCostsIndex];
            }
        }
        

        nextCost.Value = processedNextCost;
    }

    

    

}
