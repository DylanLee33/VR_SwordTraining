using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStates : MonoBehaviour {
	public float timer = 0f;
	public enum States{Idle, Defend, Attack};
	public States currentState;

	public GameObject shield;
	public GameObject projectile;

	GameObject proj1;
	GameObject proj2;
	GameObject proj3;

	bool hasStarted = false;

	GameObject player;

	public bool moveBack;

	Transform defaultPos;

	float timeCounter = 0;
	// Use this for initialization
	void Start () {
		currentState = States.Idle;
		player = GameObject.FindGameObjectWithTag ("MainCamera");

		defaultPos = transform;

		moveBack = false;
	}
	
	// Update is called once per frame
	void Update () {
		StateTimer ();

		if (currentState == States.Idle) {
			IdleState ();
		}
		if (currentState == States.Defend) {
			DefendState ();
		}
		if (currentState == States.Attack) {
			AttackState ();
		}
		
		if (transform.position == defaultPos.position) {
			moveBack = false;
		}

		if (moveBack) {
			Debug.Log ("Is moving back");
			transform.position = Vector3.MoveTowards (transform.position, defaultPos.position, Time.deltaTime * 3f);
		}

	}

	IEnumerator SpawnCubes() {
		hasStarted = true;
		proj1 = Instantiate (projectile, transform.position, transform.rotation);
		yield return new WaitForSeconds (1f);
		proj2 = Instantiate (projectile, transform.position, transform.rotation);
		yield return new WaitForSeconds (1f);
		proj3 = Instantiate (projectile, transform.position, transform.rotation);
	}

	void OnTriggerEnter(Collider other) {
		//transform.position = Vector3.Lerp (transform.position, defaultPos.position, Time.deltaTime * 3f);
		moveBack = true;

		//Maybe the state changes every time the enemy is hit? For defense state, maybe when the shield is hit at a certain time? x amount of times?
		//currentState++;
	}

	void StateTimer() {
		timer += Time.deltaTime;

		if (timer >= 3f) {
			timer = 0f;
			currentState++;
			if ((int)currentState == 3) {
				currentState = 0;
			}
		}
	}

	void IdleState() {
		//GetComponent<MoveInCircle>().enabled = false;
		moveBack = true;
		//GetComponentInChildren<Renderer> ().material.SetColor("_Color", Color.blue);
		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(1f, 1f, 1f), 0.1f);
		hasStarted = false;
	}

	void DefendState() {
		moveBack = true;
		//GetComponentInChildren<MeshRenderer> ().material.color = Color.yellow;
		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(3f, 3f, 3f), 0.1f);
	}

	void AttackState() {
		//GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
		//GetComponent<MoveInCircle>().enabled = true;
		CircleMove();

		//If all the projectiles are dead, rush the player

		//if (proj1 == null && proj2 == null && proj3 == null) {
		//	transform.position = Vector3.MoveTowards (transform.position, player.transform.position, Time.deltaTime * 3f);
		//} 

		//else spawn more
		//else {
		//	hasStarted = false;
		//}


		if (!hasStarted) {
			StartCoroutine ("SpawnCubes");
		}
		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 0.1f);
	}

	void CircleMove() {
		timeCounter += Time.deltaTime;

		float x = -3.6f;
		float y = 1.3f + Mathf.Cos(timeCounter);
		float z = Mathf.Sin(timeCounter);

		transform.position = new Vector3 (x, y, z);
	}
}
