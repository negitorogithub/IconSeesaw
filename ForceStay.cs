using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ForceStay : MonoBehaviour {


	private Vector3 firstPos;
	private Vector3 currentPos;
    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Initialize () {
		Transform t = GetComponent<Transform> ();
		firstPos = new Vector3(t.position.x, t.position.y, t.position.z);
	}

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void LateUpdate () {
		if (transform.position != firstPos) {
			transform.position = firstPos;
		}
	}
}
