using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	public GameObject planet;

	public float rocketThrust;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (Input.GetKey ("space")) {
			Vector3 playerVector = (transform.position - planet.transform.position).normalized;
			rb.AddForce ( playerVector * rocketThrust, ForceMode.Force);
		}
	}
}
