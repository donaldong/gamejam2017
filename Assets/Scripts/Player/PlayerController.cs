using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference
//  https://docs.unity3d.com/Manual/OculusControllers.html

public class PlayerController : OVRPlayerController
{
    public float raycastDistance = 10.0f;
    public GameObject text1;
    public int ObjectiveCountSpellAoe;
    public int ObjectiveCountSpellIndex;
    public int ObjectiveCountWeaponThrow;
    public int ObjectiveCountEnemyKill;

    [HideInInspector]
    public RaycastHit eyeHit;
    [HideInInspector]
    public bool bEyeHit;
    [HideInInspector]
    public OVRCameraRig OVRCamera;
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public TextMesh debugMenu;
    [HideInInspector]
    public int countSpellAoe;
    [HideInInspector]
    public int countSpellIndex;
    [HideInInspector]
    public int countEnemyKill;
    [HideInInspector]
    public int countWeaponThrow;
    [HideInInspector]
    public float damageTaken;

    protected new void Awake()
    {
        base.Awake();
        OVRCamera = GetComponentInChildren<OVRCameraRig>();
        weapon = GetComponentInChildren<Weapon>();
        debugMenu = text1.GetComponent<TextMesh>();
        countSpellAoe = 0;
        damageTaken = 0.0f;
        countSpellIndex = 0;
        countEnemyKill = 0;
        countWeaponThrow = 0;
        Enemy.pc = this;
        Spell.pc = this;
        Weapon.pc = this;
        TresureBox.pc = this;
    }

    protected void Start()
    {
        TresureBox.t0.objective = ObjectiveCountSpellAoe;
        TresureBox.t1.objective = ObjectiveCountSpellIndex;
        TresureBox.t2.objective = ObjectiveCountWeaponThrow;
        TresureBox.t3.objective = ObjectiveCountEnemyKill;
    }

    public void FixedUpdate()
    {
        bEyeHit = Physics.Raycast(OVRCamera.centerEyeAnchor.position, 
            OVRCamera.centerEyeAnchor.forward, out eyeHit, raycastDistance);
    }

    public void Update()
    {


        if (TresureBox.t0.left <= 0)
            TresureBox.t0.Open();
        if (TresureBox.t1.left <= 0)
            TresureBox.t1.Open();
        if (TresureBox.t2.left <= 0)
            TresureBox.t2.Open();
        if (TresureBox.t3.left <= 0)
            TresureBox.t3.Open();
        if (TresureBox.t4.left <= 0)
            TresureBox.t4.Open();
    }
}
