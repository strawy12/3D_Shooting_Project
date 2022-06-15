using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCameraFeedBack :FeedBack
{
    [SerializeField] private CinemachineVirtualCamera _cmVCam;

    [SerializeField]
    [Range(0f, 5f)] 
    private float _amplitude = 1, _intensity = 1;

    [SerializeField]
    [Range(0f, 1f)]
    private float _duration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void OnEnable()
    {
        if (_cmVCam == null)
        {
            _cmVCam = Define.VCam;
        }

        _noise = _cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CompletePrevFeedBack()
    {
        StopAllCoroutines();

        _noise.m_AmplitudeGain = 0;
    }

    public override void CreateFeedBack()
    {
        _noise.m_AmplitudeGain = _amplitude;
        _noise.m_FrequencyGain = _intensity;

        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while(time > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }

        _noise.m_AmplitudeGain = 0; 
    }

}
