using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerLifeTime : MonoBehaviour {

	void Update () {
		if (transform.parent == null) {
			Destroy (gameObject, 5f);
		}
	}
}
