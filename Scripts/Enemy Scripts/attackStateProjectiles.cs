using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attackStateProjectiles : MonoBehaviour {
	
	GameObject player;
	public GameObject sparks;
	public MeshRenderer projectile;

	Vector3 targetPos;

	float timer = 0f;

	//SOUNDS
	private AudioSource source;
	public AudioClip explosion;

    //SCORE
    public Canvas canvas;
    private SceneTimer scenetimer;

	private WeaponCollisionManager manager;

    void Start () {
		sparks.SetActive (false);
		targetPos = new Vector3 (transform.position.x, Random.Range (0.2f, 1.8f), Random.Range (-1f, 1f));

		player = GameObject.FindGameObjectWithTag ("MainCamera");

		source = GetComponent<AudioSource> ();

		if (SceneManager.GetActiveScene ().name == "Parry" || SceneManager.GetActiveScene ().name == "Throwing") {
			scenetimer = GameObject.Find ("GameManager").GetComponent<SceneTimer> ();
		}
    }
	

	void Update () {
		timer += Time.deltaTime;
		if (timer <= 5) {
			transform.position = Vector3.MoveTowards (transform.position, targetPos, Time.deltaTime * 2f);
		}
		else {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, Time.deltaTime * 2f);
			transform.Rotate (Vector3.up * Time.deltaTime * 1000f);
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("MainCamera")) {
			player.GetComponent<PlayerHealth>().TakeDamage();
		}
		projectile.GetComponent<TrailRenderer> ().enabled = false;
		projectile.enabled = false;
		sparks.SetActive (true);

		//SOUNDS
		source.clip = explosion;
		source.PlayOneShot (explosion);

        //SCORE
		if (scenetimer != null) {
			scenetimer.AddScore ();
		}

		if (other.CompareTag ("Weapon")) {
			manager = other.transform.GetComponentInParent<WeaponCollisionManager> ();
			manager.timer = 0.5f;
		}

		gameObject.GetComponent<BoxCollider> ().enabled = false;
		Destroy (gameObject, explosion.length);
	}
}
