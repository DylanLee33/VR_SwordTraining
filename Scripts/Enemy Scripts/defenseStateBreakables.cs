using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defenseStateBreakables : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (!other.transform.IsChildOf (this.gameObject.transform) && !other.CompareTag ("Enemy")) {	//Don't collide with any children
			if (transform.childCount != 0) {
				gameObject.transform.GetChild (0).GetComponent<RegainControl> ().ForcePull ();			//Get the first child and activate force pull
				transform.DetachChildren ();															//Unparent self
				gameObject.SetActive (false);															//Turn off
			}
		}
	}
}
