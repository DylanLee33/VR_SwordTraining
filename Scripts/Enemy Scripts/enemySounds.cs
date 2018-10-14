using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySounds : MonoBehaviour {

	//SOUNDS
	private AudioSource source;
	private AudioSource stateSource;
	public AudioClip[] beepSounds;
	public AudioClip stateSound01;
	public AudioClip stateSound02;
	public AudioClip stateSound03;
	private float soundTimer = 5f;

	bool canPlaySound = true;

	void Start () {
		
		source = GetComponent<AudioSource> ();
		stateSource = gameObject.AddComponent<AudioSource> ();
		stateSource.volume = 0.5f;
	}
	

	void Update () {
		PlaySounds ();
	}


	//SOUND FUNCTION
	void PlaySounds() {
		
		soundTimer -= Time.deltaTime;

		if (soundTimer <= 0f) {
			source.clip = (beepSounds [Random.Range(0, beepSounds.Length -1)]);
			soundTimer = Random.Range (source.clip.length + 10f, source.clip.length + 15f);
			source.Play ();
		}
	}


	public void StateSound01(){
		if (!stateSource.isPlaying && canPlaySound) {
			stateSource.clip = stateSound01;
			stateSource.Play ();
			canPlaySound = false;
		}
	}

	public void StateSound02(){
		if (!stateSource.isPlaying && canPlaySound) {
			stateSource.clip = stateSound02;
			stateSource.Play ();
			canPlaySound = false;
		}
	}

	public void StateSound03(){
		if (!stateSource.isPlaying && canPlaySound) {
			stateSource.clip = stateSound03;
			stateSource.Play ();
			canPlaySound = false;
		}
	}

	public void ToggleBool() {
		canPlaySound = true;
	}

}
