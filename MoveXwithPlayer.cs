using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveXwithPlayer : MonoBehaviour {

	private Transform myTransform;
	public Transform player;
    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void Initialize ()
	{
		myTransform = GetComponent<Transform>();
	}

	void Update ()
	{
		myTransform.position = new Vector3(player.position.x , myTransform.position.y, myTransform.position.z);
	}
}
