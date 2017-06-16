using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define spells which player can use (attached to Player)
public class Spell : MonoBehaviour
{

    public enum Trigger { PrimaryIndex, SwordHoldingUp }

    public float damage = 30.0f;
    public float impact = 500.0f;
    public GameObject spell;
    public float coolDown = 1.0f;
    public float lifeSpan = 2.0f;

    protected float _countDown;

    [HideInInspector]
    public static PlayerController pc;

    protected void Awake()
    {
        _countDown = coolDown;
    }

    public virtual void OnHit(Trigger t, Vector3 dir, Vector3 pos, Enemy enemy)
    {
    }

    public void Update()
    {
        // count down
        if (_countDown > 0)
        {
            _countDown -= Time.deltaTime;
        }
    }
}