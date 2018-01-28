using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ForceStay : MonoBehaviour {

    private Vector3 firstPos;
    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Initialize()
    {

        firstPos = transform.position;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void LateUpdate()
    {
        if (transform.position != firstPos)
        {
            transform.position = firstPos;
        }
    }
}
