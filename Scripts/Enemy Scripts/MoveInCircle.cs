using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour {
	float timeCounter = 0;
	

	void Update () {
		timeCounter += Time.deltaTime;

		float x = -3.6f;
		float y = 1.3f + Mathf.Cos(timeCounter);
		float z = Mathf.Sin(timeCounter);

		transform.position = new Vector3 (x, y, z);
	}
}
