using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour {

    public GameObject weapon;
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch))
        {
            weapon.GetComponent<Weapon>().held = true;
            weapon.SetActive(true);
            Destroy(gameObject);
        }
    }
}
