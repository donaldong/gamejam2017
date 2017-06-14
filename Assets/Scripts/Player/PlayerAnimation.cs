using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public enum Spell { PrimaryIndexTrigger };

    public GameObject spellPrimaryIndexTrigger;
    public float lifeSpanPIT = 2.0f;

    // a dictionary for tracking PrimaryIndexTrigger spell
    protected Dictionary<GameObject, float> _animations_pit;
    protected OVRCameraRig _camera;

    public void Awake()
    {
        _animations_pit = new Dictionary<GameObject, float>();
        _camera = GetComponentInChildren<OVRCameraRig>();
    }

    public void Play(Spell spell)
    {
        switch (spell)
        {
            case Spell.PrimaryIndexTrigger:
                _animations_pit.Add(Instantiate(spellPrimaryIndexTrigger, _camera.leftHandAnchor), 0f);
                break;
        }
    }

    public void Update()
    {
        // destroy finished animations
        List<GameObject> keys = new List<GameObject>(_animations_pit.Keys);
        foreach (GameObject spellAnimation in keys)
        {
            _animations_pit[spellAnimation] += Time.deltaTime;
            if (_animations_pit[spellAnimation] >= lifeSpanPIT)
            {
                _animations_pit.Remove(spellAnimation);
                Destroy(spellAnimation);
            }
        }
    }
}
