using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanEffectSound : AudioPlayer
{
    [SerializeField] private AudioClip _explosionClip,
                                       _buffClip;

    public  void PlayExplosionSound()
    {
        PlayClip(_explosionClip);
    }
    public void PlayBuffSound()
    {
        PlayClipWithVariablePitch(_buffClip);
    }
}
