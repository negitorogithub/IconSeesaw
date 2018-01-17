using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using System.Collections;

public class LoadMasterData : MonoBehaviour
{

    public static MasterData masterData;

    [Serializable]
    public class MasterData
    {
        public int[]costs2LevelUp;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void Awake()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Initialize()
    {
        masterData = new MasterData();
        masterData.costs2LevelUp = new int[1000];
        String dataJson = (Resources.Load("SeasawCosts") as TextAsset).text;
        Debug.Log(dataJson);
        masterData = JsonUtility.FromJson<MasterData>(dataJson);

        
    }

}
