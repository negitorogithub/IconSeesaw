using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class TextZoom : MonoBehaviour
{
    public GameObject gameObject2Watch;
    private Text thisText;
    private int originSize;

    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void Initialize()
    {
        thisText = gameObject.GetComponent<Text>();
        thisText.fontSize = (thisText.resizeTextMaxSize/ 5) * 3;
        originSize = thisText.fontSize;

        gameObject2Watch.GetComponent<OnBallStoppedSender>().sender.First().Subscribe(

            _ => LeanTween.value(thisText.gameObject, thisText.fontSize, thisText.resizeTextMaxSize, 0.6f).setOnUpdate
                (
                    (float newValue) => { thisText.fontSize = (int)newValue; }
                )
                   
    //LeanTween.scale(gameObject, new Vector3(70f, 50f), 5.0f)
            //thisText.fontSize = (int) LeanTween.value(thisText.fontSize, 60f, 5.0f).lastVal
            );

        GamePresentator.retrySender.Subscribe(
            _ => ResetFontSize()
            );       
    }

    private void ResetFontSize() => thisText.fontSize = originSize;
}
