using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class ConstPositionClamp : MonoBehaviour
{

    public float RestrictXFrom;
    public bool IsRestrictXBigger;
    public float RestrictYFrom;
    public bool IsRestrictYBigger;
    public bool IsClampingX { get; private set; }
    public bool IsClampingY { get; private set; }

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
        IsClampingX = false;
        IsClampingY = false;
    }

    private void Update()
    {
        transform.position = Restrict(transform.position);
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

        if (vector2Proceeded.x == RestrictXFrom)
        {
            IsClampingX = true;
        }
        else
        {
            IsClampingX = false;
        }
        if (vector2Proceeded.y == RestrictYFrom)
        {
            IsClampingY = true;
        }
        else
        {
            IsClampingY = false;
        }
        return vector2Proceeded;

    }
}
