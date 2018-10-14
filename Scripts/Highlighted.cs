using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Reflection;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;

public class Highlighted : Selectable {

	public string sceneName;
	BaseEventData baseEvent;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}


	void Update()
	{
		FindControllers ();

		if (IsHighlighted (baseEvent) == true) 
		{
			if (Controller.GetHairTriggerDown ()) 
			{
				SceneManager.LoadScene (sceneName);
				Debug.Log ("PleaseWork");
			}
		}
	}


	void FindControllers()
	{
		if (trackedObj == null) 
		{
			GameObject o = GameObject.Find ("Controller (right)");
			if (o != null) {
				trackedObj = o.GetComponent<SteamVR_TrackedObject> ();
			}
		}
	}
}
