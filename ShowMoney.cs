using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class ShowMoney : MonoBehaviour
{
    public DataHolder dataHolder;

    private Text thisText;
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
        thisText = GetComponent<Text>();
        dataHolder.playerMoney.Subscribe(
            value => thisText.text = value.ToString() + "pt"
            );
    }

}
