using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Reflection;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;

public class ButtonManager : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller

	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}


    void Update()
    {
		if (trackedObj == null) 
		{
			GameObject o = GameObject.Find ("Controller (left)");
			Debug.Log ("game controller: " + o);
			if (o != null) {
				trackedObj = o.GetComponent<SteamVR_TrackedObject> ();
				Debug.Log ("FOUND game controller: " + trackedObj);
			}
		}



		if (Controller.GetHairTriggerDown())
		{
			Debug.Log ("Poop");
		}
    }


    public void DroidScene()
    {
        SceneManager.LoadScene("Duel");
    }


    public void QuitApplication()
    {
		if (Controller.GetHairTriggerDown())
		{
			Debug.Log ("Poop");
		}
    }
}
