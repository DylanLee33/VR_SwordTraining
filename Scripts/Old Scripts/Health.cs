using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	private RegainControl regaincontrol;

	public GameObject object01;
	private GameObject object02;

	void Start()
	{

	}


	void OnTriggerEnter (Collider other)
	{
		regaincontrol = other.gameObject.GetComponent<RegainControl> ();

		if (other.tag == ("Weapon")) { 		
			regaincontrol.SpawnGhostHand ();
		}


        //Disable controller actions here
        other.enabled = false;

        /*foreach (Component comp in ghostHand.GetComponents<Component>())    //Remove components from ghost hand so it isn't controlled by player
        {
            if (comp != GetComponent<Transform>() || comp != GetComponent<Rigidbody>() || comp != GetComponent<SphereCollider>())
            {
                Destroy(comp);                                                  //This might be unnecessary if only the sword is spawned and not the controller.
            }
        }
*/
		//Destroy (gameObject);     //Kill the box
	}
}
