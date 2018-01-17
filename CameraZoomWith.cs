using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class CameraZoomWith : MonoBehaviour
{
    public GameObject gameObject2Attach;

    private ConstPositionClamp constPositionClamp;
    private IsTouchnigWith isTouchnigWith;
    private float zoomOffset;
    private Transform originCameraTransform;
    private float originCameraSize;
    private float deltaBetweenCameraAndTarget;
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
        originCameraTransform = Camera.main.transform;
        originCameraSize = Camera.main.orthographicSize;
        zoomOffset = 3.0f;
        isTouchnigWith = GetComponent<IsTouchnigWith>();
        constPositionClamp = GetComponent<ConstPositionClamp>();
    }
    private void Update()
    {
        deltaBetweenCameraAndTarget = Mathf.Abs(gameObject2Attach.transform.position.y - originCameraTransform.position.y);

        if (deltaBetweenCameraAndTarget + zoomOffset > Camera.main.orthographicSize)
        {
            if (! MoveRigidbody2DWithTouch.isTouching)
            {
                Camera.main.orthographicSize = deltaBetweenCameraAndTarget + zoomOffset;
            }
        }
            //プレイヤー発射前でカメラ拡大後で差分とカメラサイズの差がZoomoffset以上
        if (!(MoveRigidbody2DWithTouch.isTouching)&(originCameraSize < Camera.main.orthographicSize)&(deltaBetweenCameraAndTarget + zoomOffset < Camera.main.orthographicSize))
        {
                Camera.main.orthographicSize = deltaBetweenCameraAndTarget + zoomOffset;
            
        }

        
        if (MoveRigidbody2DWithTouch.isTouching)
        {
            Camera.main.orthographicSize = originCameraSize;
        }

        if (isTouchnigWith.IsTouching)
        {
            Camera.main.orthographicSize = originCameraSize;

        }
        if (constPositionClamp.IsClampingY)
        {
            Camera.main.orthographicSize = originCameraSize;
        }
        
    }

}
