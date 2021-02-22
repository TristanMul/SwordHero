using UnityEngine;
using System.Collections;

public class WaterSafing : MonoBehaviour {
	public float power;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider col)
	{
		if (col.tag == "Physical") {
			col.GetComponent<Rigidbody> ().AddForce (transform.up * power);
		}

		if (col.tag == "Player") {
			if (Input.GetKey (KeyCode.Space)) {
				col.GetComponent<CharacterController> ().skinWidth = 3;
			} else {
				col.GetComponent<CharacterController> ().skinWidth = 0.08f;
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") {
			col.GetComponent<CharacterController> ().skinWidth = 0.08f;
		}
	}
}