using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotation : MonoBehaviour {

	private Vector3 newRot;
	
	void Update () {
		newRot += new Vector3(0,1,0);
		transform.localRotation = Quaternion.Euler (newRot);
	}
}
