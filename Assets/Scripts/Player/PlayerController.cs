using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference
//  https://docs.unity3d.com/Manual/OculusControllers.html

public class PlayerController : OVRPlayerController
{
    protected PlayerAnimation _animation;

    public new void Awake()
    {
        base.Awake();
        _animation = GetComponent<PlayerAnimation>();
    }

    public void Update()
    {
        if (_animation.IsPlayable(PlayerAnimation.Spell.PrimaryIndexTrigger))
        {
            _animation.Play(PlayerAnimation.Spell.PrimaryIndexTrigger);
        }
    }
}
