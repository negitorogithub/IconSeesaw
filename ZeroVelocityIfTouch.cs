using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ZeroVelocityIfTouch : MonoBehaviour {

	private FixedJoint2D fixedJoint;	//　FixedJoint
	private GameObject attachedGameObject;
    private Rigidbody2D attatchedRigidBody2D;
    private float originGravityScaleOfAttachedRigidbody2D;
    
	public bool useBreakForce;
	public float breakForce;	//　外れる力
	public bool useBreakTorque;
	public float  breakTorque;		
	public float breakRotationZFrom;
	public float breakRotationZTo;
	public Rigidbody2D rigidbodyToFix;


	void OnCollisionEnter2D (Collision2D collision)
	{
		if (rigidbodyToFix == collision.rigidbody)
		{
			if (fixedJoint == null & !((breakRotationZFrom < rigidbodyToFix.transform.localEulerAngles.z) & (rigidbodyToFix.transform.localEulerAngles.z < breakRotationZTo)))
			{
				attachedGameObject = this.gameObject;
				attachedGameObject.AddComponent<FixedJoint2D> ();
				fixedJoint = GetComponent<FixedJoint2D> ();
                attatchedRigidBody2D = GetComponent<Rigidbody2D>();
				fixedJoint.connectedBody = collision.rigidbody;
                originGravityScaleOfAttachedRigidbody2D = attatchedRigidBody2D.gravityScale;
                attatchedRigidBody2D.gravityScale = 0;
                attatchedRigidBody2D.velocity = Vector2.zero;
                attatchedRigidBody2D.angularVelocity = 0.0f;
                if (useBreakForce) {
					fixedJoint.breakForce = breakForce;
				}
				if (useBreakTorque) {
					fixedJoint.breakTorque = breakTorque;
				}
			}
		}
	}


	void Update ()
	{
		if (fixedJoint != null) 
		{
            
            if ((breakRotationZFrom<  rigidbodyToFix.transform.localEulerAngles.z)&(rigidbodyToFix.transform.localEulerAngles.z<breakRotationZTo)) 
			{
				Debug.Log ("BreakFixedJoint");
                Destroy(fixedJoint);
                GamePresentator.isPlayerFixed = false;
                attatchedRigidBody2D.gravityScale = originGravityScaleOfAttachedRigidbody2D;
			}
		}
	}

	void OnJointBreak2D (Joint2D brokenJoint)
	{
	}
}
