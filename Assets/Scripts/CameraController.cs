using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public GameObject planet;
		
	public float heightFromGround;
	public float distanceToPlayer;
	public float damp;
	public float rotationDamp;
	public float minSpeed;

	private Rigidbody playerRb;
	
	// Use this for initialization
	void Start () {
		playerRb = player.GetComponent<Rigidbody>();
		updateCamera ();
	}

	void FixedUpdate () {
		if (playerRb.velocity.magnitude > minSpeed) {
			updateCamera();
		}
	}

	void updateCamera () {
		Vector3 playerVector = (player.transform.position - planet.transform.position).normalized;
		Vector3 cameraVector = (transform.position - planet.transform.position).normalized;
		Vector3 cameraDirection = (player.transform.position - transform.position).normalized;
		Vector3 normalCameraDirection = cameraDirection - Vector3.Dot (cameraDirection, playerVector) * playerVector;
		normalCameraDirection = normalCameraDirection.normalized;

		Vector3 targetPosition = player.transform.position +
			playerVector*heightFromGround - normalCameraDirection*distanceToPlayer;
			
		float distanceToTarget = (transform.position - targetPosition).magnitude;
		transform.position = Vector3.Lerp(transform.position, targetPosition, damp * Time.time);
		transform.LookAt(player.transform, cameraVector);

	}
}
