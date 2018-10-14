using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUnusedWeapons : MonoBehaviour {

	void Update () {
		if (transform.childCount == 3) {
			Destroy (this.gameObject);
		}
	}
}
