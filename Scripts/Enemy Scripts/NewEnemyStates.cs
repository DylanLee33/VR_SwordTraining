using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewEnemyStates : MonoBehaviour {

	//States
	public enum States{Throwing, Dashing, Clashing, Blocking, Slamming};
	public States currentState;

	//Projectiles
	public GameObject projectile;

	bool hasStarted = false;

	//Characters
	GameObject player;
	public GameObject shield;

	//Return Positions
	public bool moveBack;
	public bool shieldMoveBack;

	Vector3 defaultPos;
	Vector3 shieldDefaultPos;

	//Hits
	public GameObject clashPos;
	private GameObject sparkClashing;

	//Timers
	float timeCounter = 0;
	float timer = 0;

	//Throw at these
	public GameObject[] shieldNodes;
	GameObject[] weapons;
	RegainControl[] regain;
	bool regainControlSet = false;
	bool spawnNodes = false;

	//Sound things
	private enemySounds enemysounds;
	public MusicManager music;

	//Vibration
	private WeaponCollisionManager manager;

	//Win stuff
	public GameObject wintext;
	private float counter;

	void Start () {
		wintext.SetActive (false);

		enemysounds = GetComponent<enemySounds> ();
		music.PlayAttack ();

		currentState = States.Throwing;
		player = GameObject.FindGameObjectWithTag ("MainCamera");

		defaultPos = transform.position;
		shieldDefaultPos = shield.transform.position;

		moveBack = false;
		shieldMoveBack = false;

		weapons = GameObject.FindGameObjectsWithTag ("Weapon");

		counter = 0f;

		regain = new RegainControl[weapons.Length];
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i].GetComponent<RegainControl> () != null) {
				Debug.Log ("Regain: " + regain + " Weapons(" + i + "):" + weapons);
				regain [i] = weapons [i].GetComponent<RegainControl> ();
			}
		}
	}
	

	void Update () {
		if (counter >= 3f) {
			StartCoroutine(GameOver ());
		}

		if (currentState == States.Throwing) {
			StateThrowing ();
		}
		if (currentState == States.Dashing) {
			StateDashing ();
		}
		if (currentState == States.Clashing) {
			StateClashing ();
		}
		if (currentState == States.Blocking) {
			StateBlocking ();
		}
		if (currentState == States.Slamming) {
			StateSlamming ();
		}


		if (moveBack) {
			transform.position = Vector3.MoveTowards (transform.position, defaultPos, Time.deltaTime * 3f);
		}

		if (Vector3.Distance (transform.position, defaultPos) < 0.02f) {
			moveBack = false;
		}

		if (shieldMoveBack) {
			shield.transform.position = Vector3.MoveTowards (shield.transform.position, shieldDefaultPos, Time.deltaTime * 3f);
		}

		if (Vector3.Distance(shield.transform.position, shieldDefaultPos) < 0.02f) {
			shieldMoveBack = false;
		}
	}


	void StateThrowing() {
		enemysounds.StateSound01 ();

		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(1f, 1f, 1f), 0.1f);

		if (!moveBack) {
			CircleMove ();
		}

		if (!hasStarted) {
			StartCoroutine ("SpawnCubes");
		}
	}


	void CircleMove() {
		timeCounter += Time.deltaTime;

		float x = -3.6f;
		float y = 1.3f + Mathf.Cos(timeCounter);
		float z = Mathf.Sin(timeCounter);

		transform.position = new Vector3 (x, y, z);
	}


	IEnumerator SpawnCubes() {
		hasStarted = true;

		for (int i = 0; i < 3; i++) {
			yield return new WaitForSeconds (3f);
			Instantiate (projectile, transform.position, transform.rotation);
		}
		/*
		yield return new WaitForSeconds (3f);
		Instantiate (projectile, transform.position, transform.rotation);
		yield return new WaitForSeconds (1f);
		Instantiate (projectile, transform.position, transform.rotation);
		yield return new WaitForSeconds (1f);
		Instantiate (projectile, transform.position, transform.rotation);
		*/
		yield return new WaitForSeconds (9f);
		currentState = States.Dashing;							//Rush the player!
		hasStarted = false;
		enemysounds.ToggleBool ();
	}


	void StateDashing() {
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, Time.deltaTime * 3f);

		enemysounds.StateSound02();
	}


	void StateBlocking() {
		//Move back to default position behind the shield. Lerp the shield to be big
		moveBack = true;
		shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(3f, 3f, 3f), 0.001f);

		//Activate the nodes to be thrown at. Only activate them once, rather than every frame.
		if (!spawnNodes) {
			foreach (GameObject node in shieldNodes) {
				node.SetActive (true);
			}
			spawnNodes = true;
		}
			
		if (!regainControlSet) {
			foreach (RegainControl weapon in regain) {
				if (weapon != null) {
					weapon.SpawnGhostHand ();
				}
			}
			regainControlSet = true;
		}

		Timer();		//Count to 5. If it reaches 5, slam shield into player.

		//Check if the player has destroyed the nodes before the timer hits 5. If so, attack again while shield is down.
		int nodeCount = 0;
		foreach (GameObject node in shieldNodes) {
			if (!node.activeInHierarchy) {
				nodeCount++;
			}
		}

		if (nodeCount == 2) {
			nodeCount = 0;
			currentState = States.Throwing;
			spawnNodes = false;
			regainControlSet = false;
			enemysounds.ToggleBool ();
			music.PlayAttack ();
		}
	}


	void Timer() {
		timer += Time.deltaTime;
		if (timer >= 5f) {
			currentState = States.Slamming;
			enemysounds.ToggleBool ();
			timer = 0f;
		}
	}


	void StateSlamming() {
		moveBack = true;
		//Slam the shield towards the player!
		shield.transform.position = Vector3.MoveTowards (shield.transform.position, player.transform.position, Time.deltaTime * 2);
		//When it's hit, change state

		enemysounds.StateSound03 ();
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject != shield && !other.gameObject.CompareTag ("MainCamera")) {
			if (currentState == States.Dashing) {
				transform.SetParent (other.transform);
				clashPos = other.transform.Find ("ClashPos").gameObject;
				sparkClashing = clashPos.transform.GetChild(0).gameObject;
				currentState = States.Clashing;
				enemysounds.ToggleBool ();
			}

			if (currentState == States.Clashing) {
				if (gameObject.transform.parent != other.transform) {
					Debug.Log (other.gameObject);
					transform.parent = null;
					currentState = States.Blocking;
					enemysounds.ToggleBool ();
					sparkClashing.SetActive (false);
					music.PlayDefend ();
					counter += 1f;
				}

				if (other.CompareTag ("Weapon")) {
					manager = other.transform.GetComponentInParent<WeaponCollisionManager> ();
					manager.timer = 0.5f;
				}
			}
		} else if (other.gameObject.CompareTag ("MainCamera") && currentState != States.Clashing) {
			player.GetComponent<PlayerHealth>().TakeDamage();
			moveBack = true;
			currentState = States.Throwing;
		}
	}


	void StateClashing() {
		transform.position = clashPos.transform.position;
		transform.rotation = clashPos.transform.rotation;
		sparkClashing.SetActive (true);
	}

	IEnumerator GameOver() {
		wintext.SetActive (true);
		yield return new WaitForSeconds (3);
		wintext.SetActive (false);
		SceneManager.LoadScene ("Menu");
	}
}
