using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : AudioPlayer
{
    [SerializeField]
    private AudioClip _effectClip;
    [SerializeField] private bool _useRandomPitch;
    public void PlayEffectSound()
    {
        if (_useRandomPitch)
        {
            PlayClipWithVariablePitch(_effectClip);
        }

        else
        {
            PlayClip(_effectClip);
        }
    }

}


