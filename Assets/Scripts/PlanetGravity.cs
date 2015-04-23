using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {
	public float gravity;	
	public GameObject planet;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		Vector3 playerVector = (transform.position - planet.transform.position).normalized;
		rb.AddForce (-playerVector * rb.mass * gravity);
	}
}
