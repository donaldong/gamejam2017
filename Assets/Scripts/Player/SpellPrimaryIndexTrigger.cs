using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPrimaryIndexTrigger : Spell
{
    // a dictionary for tracking PrimaryIndexTrigger spell
    protected Dictionary<GameObject, float> _animations;
    protected OVRCameraRig _camera;
    protected float _countDown;

    public void Awake()
    {
        _animations = new Dictionary<GameObject, float>();
        _camera = GetComponentInChildren<OVRCameraRig>();
    }

    private bool _playable()
    {
        if (_countDown <= 0)
        {
            _countDown = coolDown;
            return Input.GetMouseButtonDown(0) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)
                && !OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
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
        List<GameObject> keys = new List<GameObject>(_animations.Keys);
        foreach (GameObject spellAnimation in keys)
        {
            _animations[spellAnimation] += Time.deltaTime;
            if (_animations[spellAnimation] >= lifeSpan)
            {
                _animations.Remove(spellAnimation);
                Destroy(spellAnimation); 
            }
        }

        if (_playable())
        {
            var i = Instantiate(spell, _camera.leftHandAnchor);
            i.transform.localPosition = Vector3.zero;
            _animations.Add(i, 0f);
        }
    }

    public override void OnHit(Trigger t, Vector3 dir, Vector3 pos, Enemy enemy)
    {
        if (t != Trigger.PrimaryIndex) return;
        enemy.OnSpellHit(damage, impact, dir, pos);
    }
}
