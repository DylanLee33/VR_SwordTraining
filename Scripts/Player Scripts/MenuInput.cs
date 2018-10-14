using UnityEngine;
using System.Collections;
using System.Reflection;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;

public class MenuInput : MonoBehaviour {

	private ButtonManager buttonManager;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller

	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Start () 
	{
		//controller = GetComponent<SteamVR_TrackedController> ();
		buttonManager = GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ();
	}
	

	void Update () 
	{
		if (Controller.GetHairTriggerDown())
		{
			buttonManager.QuitApplication ();
		}
	}
}
