using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWeapon : MonoBehaviour {

	public GameObject weapon;

	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("MainCamera")) {
			Instantiate (weapon, transform.position, transform.rotation);
		}
	}
}
