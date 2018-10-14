using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisions : MonoBehaviour {

	private WeaponCollisionManager manager;

	void Update() {
		if (transform.parent != null) {
			if (manager == null) {
				manager = transform.parent.GetComponent<WeaponCollisionManager> ();
				Debug.Log (manager);
			}
		}
	}


	void OnTriggerStay(Collider other) {
		if (other.transform != transform.parent && other != transform.GetChild(0) && !other.CompareTag("MainCamera")) {
			if (manager != null) {
				manager.shouldVibrate = true;
			}
		}
	}


	void OnTriggerExit() {
		if (manager != null) {
			manager.shouldVibrate = false;
		}
	}
}
