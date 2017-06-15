using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPrimaryIndexTrigger : Spell
{
    // a dictionary for tracking PrimaryIndexTrigger spell
    protected Dictionary<GameObject, float> _clips;

    protected new void Awake()
    {
        base.Awake();
        _clips = new Dictionary<GameObject, float>();
    }

    private bool _playable()
    {
        // Debug key: 1
        if (_countDown <= 0)
        {
            _countDown = coolDown;
            return Input.GetKey(KeyCode.Alpha1) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        }
        return false;
    }

    public void Update()
    {
        // count down
        if ( _countDown > 0 )
        {
            _countDown -= Time.deltaTime;
        }

        // destroy finished animations for pit
        List<GameObject> keys = new List<GameObject>(_clips.Keys);
        foreach (GameObject spellAnimation in keys)
        {
            _clips[spellAnimation] += Time.deltaTime;
            if (_clips[spellAnimation] >= lifeSpan)
            {
                _clips.Remove(spellAnimation);
                Destroy(spellAnimation); 
            }
        }

        if (_playable())
        {
            var i = Instantiate(spell, pc.OVRCamera.leftHandAnchor);
            i.transform.localPosition = Vector3.zero;
            _clips.Add(i, 0f);
        }
    }

    public override void OnHit(Trigger t, Vector3 dir, Vector3 pos, Enemy enemy)
    {
        if (t != Trigger.PrimaryIndex) return;
        enemy.OnSpellHit(damage, impact, dir, pos);
    }
}
