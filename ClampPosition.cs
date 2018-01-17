using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class ClampPosition : MonoBehaviour
{
    public float RestrictXFrom;
    

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
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Min(transform.position.x, RestrictXFrom),
            transform.position.y,
            transform.position.z
            );
    }
}
