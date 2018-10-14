using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisionManager : MonoBehaviour {

	public float timer;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller

	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	WeaponCollisions weaponInHand;
	public bool shouldVibrate = false;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		timer = 0f;
	}
	

	void Update () {
		/*weaponInHand = GetComponentInChildren<WeaponCollisions> ();
		if (weaponInHand != null) {
			if (shouldVibrate) {
				Controller.TriggerHapticPulse (2000);
			}
		}*/
			

		if (timer > 0f) {
			timer -= Time.deltaTime;
			Controller.TriggerHapticPulse (2000);
		}
	}
}
