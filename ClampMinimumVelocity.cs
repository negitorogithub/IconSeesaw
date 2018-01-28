using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;
using UniRx;
using System.Collections;

public class ClampMinimumVelocity : MonoBehaviour
{
    private float thisVelocity;
    private Rigidbody2D thisRigidbody2D;
    public float minimumVelocities;

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
        thisRigidbody2D = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        thisVelocity = Abs(thisRigidbody2D.velocity.x) + Abs(thisRigidbody2D.velocity.y);
        if (thisVelocity < minimumVelocities)
        {
            thisRigidbody2D.velocity = Vector2.zero;
        }
    }

}
