using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePositionWithTransform : MonoBehaviour {

	public Transform transformToMoveWith;
	public Vector2 offset;
	private Vector2 thisPos;
    
    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void Initialize () {
		thisPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		thisPos.x = transformToMoveWith.position.x + offset.x;
		thisPos.y = transformToMoveWith.position.y + offset.y;
	}
}
