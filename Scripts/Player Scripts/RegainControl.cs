using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegainControl : MonoBehaviour {

	GameObject player;
	public Transform[] swordSpawn;

	bool forcepull = false;
	public bool detachFromHand = false;


	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("MainCamera");
	}


	void Update() {
		if (forcepull) {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 2 * Time.deltaTime);
		}
		if (Vector3.Distance (transform.position, player.transform.position) <= 0.5f) {
			forcepull = false;
		}
	}


	public void SpawnGhostHand()
	{
		StartCoroutine (ParentDelay ());
	}
		

	public void ForcePull()
	{
		detachFromHand = false;
		forcepull = true;
	}


	IEnumerator ParentDelay() {
		detachFromHand = true;
		forcepull = false;

		yield return new WaitForSeconds (0.1f);

		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;

		foreach (Transform spawn in swordSpawn) {
			if (spawn.childCount == 0) {
				gameObject.transform.position = spawn.transform.position;
				gameObject.transform.parent = spawn;
				break;
			}
		}
	}
}
