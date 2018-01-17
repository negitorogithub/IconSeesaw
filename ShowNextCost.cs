using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;
using UnityEngine.UI;

public class ShowNextCost : MonoBehaviour
{

    public DataHolder dataHolder;
    public LoadMasterData loadMasterData;
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
        GamePresentator.nextCost.SubscribeToText(thisText,
            value => (value.ToString()) + "pt"
            );
    }

}
