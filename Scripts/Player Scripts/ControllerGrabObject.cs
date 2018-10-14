using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;

	// 1
	private GameObject collidingObject; 
	// 2
	private GameObject objectInHand;

	private GameObject model;

	public GameObject weapon;
	private GameObject spawnedWeapon;

	private SteamVR_Controller.Device Controller


	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		model = this.gameObject.transform.GetChild (0).gameObject;
	}

	private void SetCollidingObject(Collider col)
	{
		// 1
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}
		// 2
		collidingObject = col.gameObject;
	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	// 2
	public void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("MainCamera")) {
			if (Controller.GetHairTriggerDown ()) {
				spawnedWeapon = Instantiate (weapon, transform.position, transform.rotation);
				SetCollidingObject(spawnedWeapon.GetComponent<Collider>());
			}
		}
		//Maybe put an else here if grabbing the daggers doesn't work?
		SetCollidingObject(other);
	}

	// 3
	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject()
	{
		// 1
		objectInHand = collidingObject;
		collidingObject = null;

		objectInHand.transform.SetParent (this.transform);

		// 2
		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	// 3
	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}
		

	public Vector3 GetTrackedObjectVelocity()
	{
		if ( Controller != null )
		{
			return transform.parent.TransformVector( Controller.velocity );
		}

		return Vector3.zero;
	}


	public Vector3 GetTrackedObjectAngularVelocity()
	{
		if ( Controller != null )
		{
			return transform.parent.TransformVector( Controller.angularVelocity );
		}

		return Vector3.zero;
	}


	private void ReleaseObject()
	{
		// 1
		if (GetComponent<FixedJoint>())
		{
			// 2
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());


			objectInHand.transform.parent = null;
			// 3
			objectInHand.transform.localPosition = this.gameObject.transform.position;
			objectInHand.transform.localRotation = this.gameObject.transform.rotation;


			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;


		}
		// 4
		objectInHand = null;
	}


	// Update is called once per frame
	void Update () {
		// 1
		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject)
			{
				GrabObject();
			}
		}

		// 2
		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
				model.SetActive (true);
			}
		}

		if (objectInHand != null) {
			if (objectInHand.GetComponent<RegainControl> () != null) {
				if (objectInHand.GetComponent<RegainControl> ().detachFromHand) {
					ReleaseObject ();
					model.SetActive (true);
				} else {
					objectInHand.transform.position = this.gameObject.transform.position;
					objectInHand.transform.rotation = this.gameObject.transform.rotation;
					model.SetActive (false);
				}
			}
		}
	}
}
