using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttributes{
    public Vector3 positionOffset;
    public float angularSpeed = Mathf.Infinity;
    public int attacksPerSecond;
    public float attackDamage;
    public float health;
}