using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] protected float _pitchRandmness = 0.2f;

    protected AudioSource _audioSource;

    protected float _basePitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _basePitch = _audioSource.pitch;
    }

    // 랜덤한 피치로 조정하여 재생
    protected void PlayClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-_pitchRandmness, _pitchRandmness);
        _audioSource.pitch = _basePitch + randomPitch;
        PlayClip(clip);
    }

    // 피치 조정 없이 재생
    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
