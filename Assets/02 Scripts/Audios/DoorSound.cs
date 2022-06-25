using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : AudioPlayer
{
    [SerializeField]
    private AudioClip _openClip,  _closeClip;

    public void PlayOpenSound()
    {
        PlayClip(_openClip);
    }
    public void PlayCloseSound()
    {
        PlayClip(_closeClip);
    }
}
