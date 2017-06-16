using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwordHoldingUp : Spell
{
    public float range = 1000.0f;

    public new void Update()
    {
        base.Update();

        // Debug key: q
        if (_playable() || Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Die");
            pc.weapon.PlayAoeEffect();
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
        if (_countDown <= 0)
        {
            _countDown = coolDown;
            return pc.weapon.IsHoldingUp() && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        }
        return false;
    }
}
