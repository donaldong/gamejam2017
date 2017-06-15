using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave {
    public GameObject enemy;
    public float rate = 1.0f;
    public EnemyAttributes[] enemiesInWave;
}
