using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

    public GameObject enemyprojectile;
    public GameObject enemyprojectileFloat;
    private GameObject enemyProjectile;

    private float timeToSpawn;
    private float reducedTime;
    private float scaleAmount;

    private Vector3 targetPos;

    void Start ()
    {
        timeToSpawn = 2f;
        reducedTime = 0f;
        scaleAmount = 0.05f;
    }
	

	void Update ()
    {
        timeToSpawn -= Time.deltaTime;
        targetPos = new Vector3(transform.position.x, Random.Range(1f, 2f), Random.Range(-2f, 2f));
        InstantiateAttackProjectile();
    }


    void InstantiateAttackProjectile()
    {
        if (timeToSpawn <= 0 + reducedTime)
        {
            enemyProjectile = Instantiate(enemyprojectile, transform.position, transform.rotation);

            if (reducedTime <= 1.5f)
            {
                reducedTime += scaleAmount;
            }

            timeToSpawn = 2f;
        }
    }
}
