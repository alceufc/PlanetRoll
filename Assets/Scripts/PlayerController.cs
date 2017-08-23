using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float thrust;
	public float brakeFactor;
	public float jumpForce;

	private bool isTouchingGround;
	public GameObject planet;
	public GameObject camera;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		isTouchingGround = false;
	}
	
	void FixedUpdate () {
		if (isTouchingGround) {
			ApplyControlForces();
		}
	}

	void ApplyControlForces () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 playerVector = (transform.position - planet.transform.position).normalized;
		Vector3 cameraVector = (camera.transform.position - planet.transform.position).normalized;
		Vector3 forceNormal = Vector3.Cross (playerVector, cameraVector).normalized;
		Vector3 forceForward = Vector3.Cross (playerVector, forceNormal).normalized;
		
		
		if (moveVertical < 0.0) {
			// Brake.
			Vector3 velocity = rb.velocity.normalized;
			float velocityMag = velocity.magnitude;
			if (velocityMag > 0.0) {
				rb.AddForce (-velocity.normalized * velocityMag * velocityMag * brakeFactor);
			}
		} else {
			// Apply Thurst.
			Vector3 forceVector = (forceForward * moveVertical - forceNormal * moveHorizontal).normalized;
			Debug.DrawRay (transform.position, forceVector * 3, Color.green);
			rb.AddForce (forceVector * thrust);
		}
		
//		if (!Input.GetKey ("space")) {
//			// Add a centripetal force
//			float radius = planet.GetComponent<SphereCollider>().radius;
//
//			//Vector3 normalCameraDirection = cameraDirection - Vector3.Dot (cameraDirection, playerVector) * playerVector;
//			Vector3 tangentialSpeed = rb.velocity - Vector3.Dot (rb.velocity, playerVector) * playerVector;
//			//float speed = rb.velocity.magnitude;
//			float speed = tangentialSpeed.magnitude;
//			rb.AddForce ( -playerVector*speed*speed/radius, ForceMode.Force);
//		}
//
//		if (Input.GetKeyDown ("backspace")) {
//			gameObject.SetActive(false);
//		}


//		if (Input.GetKeyDown ("space")) {
//			rb.AddForce (playerVector * jumpForce, ForceMode.Impulse);
//		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag ("Ground")) {
			isTouchingGround = true;
		}
	}

	void OnCollisionExit(Collision other) {
		if (other.gameObject.CompareTag ("Ground")) {
			isTouchingGround = false;
		}
	}
}
