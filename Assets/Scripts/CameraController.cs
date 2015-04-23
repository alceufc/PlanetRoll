using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public GameObject planet;
		
	private Rigidbody playerRb;

	public float heightFromGround;
	public float distanceToPlayer;
	public float damp;
	public float rotationDamp;
	public float minSpeed;
	
	// Use this for initialization
	void Start () {
		playerRb = player.GetComponent<Rigidbody>();
		//lastPlayerPosition = player.transform.position;

		updateCamera ();
	}
	
	// Update is called once per frame
	//void LateUpdate () {
	void FixedUpdate () {
		//float playerSpeed = (player.transform.position - lastPlayerPosition).magnitude;

		if (playerRb.velocity.magnitude > minSpeed) {
			updateCamera();
		}

		//lastPlayerPosition = player.transform.position;
	}

	void updateCamera () {
		Vector3 speedDirection = playerRb.velocity.normalized;
		Vector3 cameraVector = (transform.position - planet.transform.position).normalized;

		Vector3 targetPosition = player.transform.position +
			cameraVector*heightFromGround - speedDirection*distanceToPlayer;
			transform.LookAt(player.transform, cameraVector);

		float distanceToTarget = (transform.position - targetPosition).magnitude;
		//float lerpFraction = Mathf.Exp (-distanceToTarget);
		float lerpFraction = 1/(1 + damp*distanceToTarget);

		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpFraction);

		transform.LookAt(player.transform, cameraVector);
//		Quaternion oldRotation = transform.rotation;
//		transform.LookAt(player.transform, cameraVector);
//		Quaternion targetRotation = transform.rotation;
//
//		float angleDiff = Vector3.Angle (oldRotation.eulerAngles, targetRotation.eulerAngles) * Mathf.PI / 180.0f;
//		transform.rotation = Quaternion.Lerp (oldRotation, targetRotation, Mathf.Cos(angleDiff));
		//transform.rotation = Quaternion.Lerp (oldRotation, targetRotation, Time.time * rotationDamp);
	}
}
