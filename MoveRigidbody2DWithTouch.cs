using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class MoveRigidbody2DWithTouch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {


	public float smoothing;
	public Rigidbody2D rigidbody2Move;
    public float RestrictXFrom;
    public bool IsRestrictXBigger;
    public float RestrictYFrom;
    public bool IsRestrictYBigger;
    public static bool isTouching { get; private set; }


    
    private float OriginCameraSize;
    private const int sizeOfVelocityHistory = 3; 
    private List<Vector2> recentVelocities;
    private float originGravityScale;
	private Vector2 lastVelocity;
	private Vector2 origin;
	private Vector2 smoothDirection;
	private Vector2 touchedPos2D;
	private Vector3 touchedPos;
    [HideInInspector]
    
	private int pointerID;



    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void Start () {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	void Initialize () {
        OriginCameraSize = Camera.main.orthographicSize;
        originGravityScale = rigidbody2Move.gravityScale;
        isTouching = false;
        recentVelocities = new List<Vector2>();
        for (int i = 0; i < sizeOfVelocityHistory; i++)
        {
            recentVelocities.Add(Vector2.zero);
        }
    }


    void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
	{
		Debug.Log("PointerDown");
		if (!isTouching)
        {
            isTouching = true;
            pointerID = eventData.pointerId;
            origin = eventData.position;
            touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 30.2f));
            touchedPos2D.x = touchedPos.x;
            touchedPos2D.y = touchedPos.y;
            touchedPos2D = Restrict(touchedPos2D);
            rigidbody2Move.isKinematic = true;
            rigidbody2Move.gameObject.layer = LayerName.MovingMoon;
            rigidbody2Move.position = (touchedPos2D);
            rigidbody2Move.velocity = Vector2.zero;
            rigidbody2Move.angularVelocity = 0;
            rigidbody2Move.gravityScale = 0.0f;
            lastVelocity = rigidbody2Move.GetPointVelocity(touchedPos2D);
            recentVelocities = pushValue(recentVelocities ,lastVelocity);
        }
    }



    void IDragHandler.OnDrag (PointerEventData eventData)
	{
		if (eventData.pointerId == pointerID) {
			touchedPos = Camera.main.ScreenToWorldPoint (new Vector3 (eventData.position.x, eventData.position.y, 30.2f));
			touchedPos2D.x = touchedPos.x;
			touchedPos2D.y = touchedPos.y;

            touchedPos2D = Restrict(touchedPos2D);

            //加速度を発生させるため
            rigidbody2Move.MovePosition (touchedPos2D);
			lastVelocity = rigidbody2Move.GetPointVelocity(touchedPos2D);
            recentVelocities = pushValue(recentVelocities, lastVelocity);
        }


		//Vector2 directionVector = Vector2.MoveTowards(transform.position, touchedPos, smoothing);

		//transform.position.Set (directionVector.x + pos.x, directionVector.y + pos.y);
	}

    private Vector2 Restrict(Vector2 fromVector2)
    {
        Vector2 vector2Proceeded = Vector2.zero;
        if (IsRestrictXBigger)
        {
            vector2Proceeded.x = Mathf.Min(fromVector2.x, RestrictXFrom);
        }
        else
        {
            vector2Proceeded.x = Mathf.Max(fromVector2.x, RestrictXFrom);
        }
        if (IsRestrictYBigger)
        {
            vector2Proceeded.y = Mathf.Min(fromVector2.y, RestrictYFrom);
        }
        else
        {
            vector2Proceeded.y = Mathf.Max(fromVector2.y, RestrictYFrom);
        }
        return vector2Proceeded;

    }
    void IPointerUpHandler.OnPointerUp (PointerEventData eventData)
	{

		Debug.Log("PointerUp");
		if (eventData.pointerId == pointerID) {
            rigidbody2Move.isKinematic = false;
            Vector2 Velocity2Add = new Vector2(recentVelocities.Select(element => element.x).Average(), recentVelocities.Select(element => element.y).Average()) ;

            rigidbody2Move.AddForce ( Velocity2Add * rigidbody2Move.mass * (originGravityScale) /5 , ForceMode2D.Impulse);
			rigidbody2Move.gravityScale = originGravityScale;
			isTouching = false;
            rigidbody2Move.gameObject.layer = LayerName.Moon;
		}
	}

    private List<Vector2> pushValue(List<Vector2> Vector2s, Vector2 newValue)
    {
        List<Vector2> Vector22Return = new List<Vector2>(Vector2s);
        Vector22Return.Add(newValue);

        if (Vector22Return.Count > sizeOfVelocityHistory)
        {
            Vector22Return.RemoveAt(0);
        }

        return Vector22Return;

    }
}