using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwordHoldingUp : Spell
{
    public float range = 1000.0f;

    protected GameObject _instance;

    public new void Update()
    {
        base.Update();

        // Debug key: q
        if (_playable() || Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Die");
            pc.weapon.PlayAoeEffect();
            Destroy(_instance);
            _instance = Instantiate(spell, null);
            _instance.transform.position = transform.position - new Vector3(0, 0.9f, 0);
            _countDown = coolDown;
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
            pc.debugMenu.text = "Spell Ready!";
            return pc.weapon.IsHoldingUp() && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        }
        pc.debugMenu.text = "AoE Cooldown: " + _countDown.ToString("n2");
        return false;
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpellSwordHoldingUp : Spell
//{
//    public float range = 1000.0f;

//    protected GameObject _instance;
//    protected float _instanceCounter = 0.0f;

//    public new void Update()
//    {
//        base.Update();

//        //if (_instanceCounter <= 0)
//        //{
//        //    Destroy(_instance);
//        //    _instance = null;
//        //}

//        // Debug key: q
//        if (_playable() || Input.GetKey(KeyCode.Q))
//        {
//            Debug.Log("Die");
//            pc.weapon.PlayAoeEffect();
//            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//            //_instance = Instantiate(spell, null);
//            //_instance.transform.localPosition = Vector3.zero;
//            foreach (GameObject enemy in enemies)
//            {
//                Vector3 dir = enemy.transform.position - transform.position;
//                if (dir.magnitude <= range)
//                    enemy.GetComponent<Enemy>().OnSpellHit(damage, impact, dir, enemy.transform.position);
//            }
//        }
//    }

//    private bool _playable()
//    {
//        if (_countDown <= 0)
//        {
//            _countDown = coolDown;
//            return pc.weapon.IsHoldingUp() && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
//        }
//        return false;
//    }
//}
