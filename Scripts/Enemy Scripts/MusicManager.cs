using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	AudioSource source;

	public AudioClip attackSong;
	public AudioClip defendSong;
	public AudioClip explosionTransition;

	AudioClip nowPlaying;

	Queue<AudioClip> q = new Queue<AudioClip>();


	void Awake () {
		source = GetComponent<AudioSource> ();
	}
	

	void Update () {
		if (source.isPlaying == false) {
			if (q.Count != 0) {
				nowPlaying = q.Dequeue ();
				source.clip = nowPlaying;
				if (nowPlaying != explosionTransition) {
					source.loop = true;
					source.Play ();
				} else {
					source.loop = false;
					source.Play ();
				}
			}
		}
	}


	public void PlayAttack() {
		q.Clear ();
		source.Stop ();

		q.Enqueue (explosionTransition);
		q.Enqueue (attackSong);
	}


	public void PlayDefend() {
		q.Clear ();
		source.Stop ();

		q.Enqueue (explosionTransition);
		q.Enqueue (defendSong);
	}
}
