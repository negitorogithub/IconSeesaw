using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class IsTouchnigWith : MonoBehaviour
{
    public Collider2D collider2D2Detect;
    private Collider2D thisCollider2D;
    [HideInInspector]
    public bool IsTouching;
    public bool IsTouchingWithSomething;
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
        thisCollider2D = GetComponent<Collider2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider == collider2D2Detect)
        {
            IsTouching = true;

        }
        IsTouchingWithSomething = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsTouchingWithSomething = false;
    }
    private void FixedUpdate()
    {
        IsTouching = false;
    }
}
