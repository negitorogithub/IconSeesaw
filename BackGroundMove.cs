using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class BackGroundMove : MonoBehaviour
{

    public Transform transform2Attach;
    private Vector3 thisSize;
    private float originX;
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
        originX = transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(
            (Mathf.Floor((transform2Attach.position.x - originX) / thisSize.x)) * thisSize.x,
            transform.position.y,
            0.0f

            );

    }
}
