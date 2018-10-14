using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPrefab;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Start() {
		laser = Instantiate (laserPrefab);
		laserTransform = laser.transform;
	}

	void Update() {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			RaycastHit hit;

			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 1000)) {
				hitPoint = hit.point;
				ShowLaser (hit);
			}
		} else {
			laser.SetActive (false);
		}
	}

	private void ShowLaser(RaycastHit hit) {
		laser.SetActive (true);
		laser.transform.position = Vector3.Lerp (trackedObj.transform.position, hitPoint, .5f);
		laserTransform.LookAt (hitPoint);
		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);

	}
}
