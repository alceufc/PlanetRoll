using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float thrust;
	public float playerMass;
	public float gravity;
	public float brakeFactor;

	public GameObject planet;
	public GameObject camera;

	private Rigidbody rb;

	//private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		//lastPosition = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 playerVector = (transform.position - planet.transform.position).normalized;
		Vector3 cameraVector = (camera.transform.position - planet.transform.position).normalized;
		Vector3 forceNormal = Vector3.Cross (playerVector, cameraVector);
		Vector3 forceVector = Vector3.Cross (playerVector, forceNormal);

		Debug.DrawRay (transform.position, forceVector*3, Color.green);
		if (moveVertical < 0.0) {
			// Brake.
			Vector3 velocity = rb.velocity.normalized;
			float velocityMag = velocity.magnitude;
			if (velocityMag > 0.0){
				rb.AddForce (-velocity.normalized * velocityMag * velocityMag * brakeFactor);
			}
		} else if (moveVertical > 0.0) {
			// Apply Thurst.
			rb.AddForce (moveVertical*forceVector*thrust);
		}

		rb.AddForce (-moveHorizontal*forceNormal*thrust);

		// Gravity.
		rb.AddForce (-playerVector*playerMass*gravity);
	}	
}
