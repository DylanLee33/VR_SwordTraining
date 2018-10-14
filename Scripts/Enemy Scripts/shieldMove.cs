using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldMove : MonoBehaviour {
	GameObject enemy;
	NewEnemyStates enemyState;


	void Start () {
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		enemyState = enemy.GetComponent<NewEnemyStates> ();
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject != enemy) {
			if (enemyState.currentState == NewEnemyStates.States.Slamming) {
				enemyState.shieldMoveBack = true;
				if (other.gameObject.CompareTag ("MainCamera")) {
					enemyState.currentState = NewEnemyStates.States.Blocking;
					other.GetComponent<PlayerHealth>().TakeDamage();
				}

				if (other.gameObject.CompareTag ("Weapon")) {
					enemyState.currentState = NewEnemyStates.States.Throwing;
				}
			}
		}
	}
}
