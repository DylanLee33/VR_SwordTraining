using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSparks : MonoBehaviour {

	public GameObject sparks;
	private GameObject Sparks;

	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter (Collider other)
	{
		Debug.Log("Play Sparks");
		Sparks = Instantiate (sparks, other.gameObject.transform.position, Quaternion.identity); //ASK GORDON HOW TO GET COLLISION POINT FOR TRIGGER
	}
}
