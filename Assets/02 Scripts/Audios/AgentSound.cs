using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    [SerializeField]
    private AudioClip _hitClip = null,
                      _deathClip = null,
                      _attackSound = null,
                      _targetAttackSound = null;

    public void PlayHitSound()
    {
        PlayClipWithVariablePitch(_hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(_deathClip);
    }
    public void PlayAttackSound()
    {
        PlayClipWithVariablePitch(_attackSound);
    }    public void PlayTargetAttackSound()
    {
        PlayClipWithVariablePitch(_targetAttackSound);
    }
}
