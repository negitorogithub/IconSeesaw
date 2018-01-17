using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class InspectorTest : MonoBehaviour
{

    public GameObject gameObject2Attach;
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
        if (gameObject2Attach == gameObject)
        {
            Debug.Log("一致");
        }
    }

}
