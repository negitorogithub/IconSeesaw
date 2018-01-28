using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class ShowHighScore : MonoBehaviour
{

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
        
    }

}
