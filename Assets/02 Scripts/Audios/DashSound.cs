using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSound : AudioPlayer
{
    [SerializeField]
    private AudioClip _dashClip;

    public void PlayDashSound()
    {
        PlayClip(_dashClip);
    }

}


