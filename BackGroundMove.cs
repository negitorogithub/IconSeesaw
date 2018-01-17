using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class BackGroundMove : MonoBehaviour
{

    public Transform transform2Attach;
    private Vector3 thisSize;
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
        thisSize = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void Update()
    {
        transform.position = new Vector3(
            (Mathf.Floor(transform2Attach.position.x / thisSize.x)) * thisSize.x,
            transform.position.y,
            0.0f

            );

    }
}
