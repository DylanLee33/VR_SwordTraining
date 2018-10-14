using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	[SerializeField] int health;

	public GameObject loseText;


	void Start () {
		health = 5;
		loseText.SetActive (false);
	}
	

	void Update () {
		if (health <= 0) {
			StartCoroutine (EndGame ());
		}
	}


	public void TakeDamage() {
		health--;
	}


	IEnumerator EndGame() {
		loseText.SetActive (true);
		yield return new WaitForSeconds (3);
		loseText.SetActive (false);
		SceneManager.LoadScene ("Menu");
	}
}
