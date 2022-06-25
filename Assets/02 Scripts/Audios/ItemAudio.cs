using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudio : AudioPlayer
{
    [SerializeField] private AudioClip _pickItemClip;

    public void PlayPickItemSound()
    {
        PlayClipWithVariablePitch(_pickItemClip);
    }
}
