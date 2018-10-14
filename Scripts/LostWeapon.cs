using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostWeapon : MonoBehaviour {
	RegainControl regain;
	public Transform[] swordSpawn;

	void OnTriggerEnter(Collider other) {
		regain = other.GetComponent<RegainControl> ();
		if (regain != null) {
			foreach (Transform spawn in swordSpawn) {
				if (!spawn.gameObject.activeInHierarchy) {
					spawn.gameObject.SetActive (true);
					break;
				}
			}
			regain.SpawnGhostHand ();
		}
	}
}
