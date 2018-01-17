using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    public float XPower;
    public float YPower;


	// Use this for initialization
	void Start () {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce (new Vector2(XPower, YPower));
	}
	

}
