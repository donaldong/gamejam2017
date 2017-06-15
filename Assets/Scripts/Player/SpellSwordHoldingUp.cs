using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwordHoldingUp : Spell
{
    public float range = 1000.0f;

    public void Update()
    {
        // Debug key: q
        if (_playable() || Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Die");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Vector3 dir = enemy.transform.position - transform.position;
                if (dir.magnitude <= range)
                    enemy.GetComponent<Enemy>().OnSpellHit(damage, impact, dir, enemy.transform.position);
            }
        }
    }

    private bool _playable()
    {
        return pc.weapon.IsHoldingUp() && pc.weapon.IsFocusedByEye();
    }
}
