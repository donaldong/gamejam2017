using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public enum Trigger { PrimaryIndex }

    public float damage = 30.0f;
    public float impact = 500.0f;
    public GameObject spell;
    public float coolDown = 1.0f;
    public float lifeSpan = 2.0f;

    public virtual void OnHit(Trigger t, Vector3 dir, Vector3 pos, Enemy enemy)
    {
    }
}