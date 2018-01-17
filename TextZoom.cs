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
    private int originFontSize;


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
        originFontSize = thisText.fontSize;
        
        
        gameObject2Watch.GetComponent<OnBallStoppedSender>().sender.First().Subscribe(

            _ => LeanTween.value(40, 90, 1.0f).setOnUpdate
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
    private void Zoom()
    {
        Debug.Log("ZoomStart");
        LeanTween.value(54, 80, 5.0f).setOnUpdate
            (
                (float newValue) => { thisText.fontSize = (int) newValue; }
            );
    }

    private void ResetFontSize() => thisText.fontSize = originFontSize;
}
