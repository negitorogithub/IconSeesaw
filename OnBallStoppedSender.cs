using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Mathf;



public class OnBallStoppedSender : MonoBehaviour

{
    private float  thisVelocity;
    private float originPosX;
    private Rigidbody2D thisRigidbody2D;
    private IsTouchnigWith IsGroundedScript;
    public Subject<Unit> sender;

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }
    private void Awake()
    {
        Initialize();
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Initialize()
    {
        

        sender = new Subject<Unit>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        IsGroundedScript = GetComponent<IsTouchnigWith>();
        thisVelocity = Abs(thisRigidbody2D.velocity.x) + Abs(thisRigidbody2D.velocity.y);
        originPosX = transform.position.x;

        Observable.FromCoroutine(MainLoop).First().TakeUntilDestroy(this).Subscribe(
            _ => Debug.Log("MainLoopコルーチン終了")
            );
        
    }

    private IEnumerator MainLoop()
    {
        
        while (true)
        {
            thisVelocity = Abs(thisRigidbody2D.velocity.x) + Abs(thisRigidbody2D.velocity.y);
            if ((thisVelocity < 0.01)&(originPosX != transform.position.x)&IsGroundedScript.IsTouching) {
                thisRigidbody2D.velocity = Vector2.zero;
                sender.OnNext(Unit.Default);
                Debug.Log("BallStopped");
            }
            yield return new WaitForSeconds(0.016f);
        }
    }


}