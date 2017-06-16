using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference
//  https://docs.unity3d.com/Manual/OculusControllers.html

public class PlayerController : OVRPlayerController
{
    public float raycastDistance = 10.0f;
    public GameObject text1;

    [HideInInspector]
    public RaycastHit eyeHit;
    public bool bEyeHit;
    public OVRCameraRig OVRCamera;
    public Weapon weapon;
    public TextMesh debugMenu;

    protected new void Awake()
    {
        base.Awake();
        OVRCamera = GetComponentInChildren<OVRCameraRig>();
        weapon = GetComponentInChildren<Weapon>();
        debugMenu = text1.GetComponent<TextMesh>();
        Enemy.pc = this;
        Spell.pc = this;
        Weapon.pc = this;
    }

    public void FixedUpdate()
    {
        bEyeHit = Physics.Raycast(OVRCamera.centerEyeAnchor.position, 
            OVRCamera.centerEyeAnchor.forward, out eyeHit, raycastDistance);
    }
}
