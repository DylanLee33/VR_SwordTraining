using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public string sceneName;
	private Vector3 newRot;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}


	void Update()
	{
		if (trackedObj == null) 
		{
			GameObject o = GameObject.Find ("Controller (right)");
			if (o != null) {
				trackedObj = o.GetComponent<SteamVR_TrackedObject> ();
			}
		}
	}


	void OnTriggerStay(Collider other)
	{
		newRot += new Vector3(0,0.5f,0);
		transform.localRotation = Quaternion.Euler (newRot);

		if (Controller.GetHairTriggerDown ()) {

			if (sceneName == "Quit") {
				Application.Quit();
			}

			SceneManager.LoadScene (sceneName);	
		}
	}
}
