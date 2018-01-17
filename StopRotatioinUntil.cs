using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class StopRotatioinUntil : MonoBehaviour
{
    public Collider2D collider2D2Detect;

    private bool isTouched;
    private Rigidbody2D thisRigidbody2D;
    private Quaternion originRotation;
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
        isTouched = false;
        originRotation = transform.rotation;
        thisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isTouched)
        {
            transform.rotation = originRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == collider2D2Detect.name)
        {
            isTouched = true;
        }
    }
}
