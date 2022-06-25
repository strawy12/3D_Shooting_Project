using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudioStep : AudioPlayer
{
    [SerializeField] protected AudioClip _stepClip;
    [SerializeField] private float _delayTime = 0.2f;

    private float _timer;
    public void PlayStepSound(float velocity)
    {
        if(velocity > 0.1f)
        {
            _timer += Time.fixedDeltaTime;

            if(_timer >= _delayTime)
            {
                _timer = 0f;
                PlayClipWithVariablePitch(_stepClip);
            }
        }

        else
        {
            _timer = _delayTime;
        }
    }
}
