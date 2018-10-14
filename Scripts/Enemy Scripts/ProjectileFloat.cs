using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFloat : MonoBehaviour {

    private MeshRenderer projectile;
    public GameObject sparks;

    //SOUNDS
    private AudioSource source;
    public AudioClip explosion;

    //SCORE
    private SceneTimer scenetimer;

    void Start()
    {
        //VFX
        projectile = GetComponent<MeshRenderer>();
        sparks.SetActive(false);

        //AUDIO
        source = GetComponent<AudioSource>();

        //SCORE
        scenetimer = GameObject.Find("GameManager").GetComponent<SceneTimer>();
    }


    void OnTriggerEnter(Collider other)
    {
        //VFX
        projectile.enabled = false;
        sparks.SetActive(true);

        //SOUNDS
        source.clip = explosion;
        source.PlayOneShot(explosion);

        //SCORE
        scenetimer.AddScore();

        //DESTROY PROJECTILE
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, explosion.length);
    }
}

