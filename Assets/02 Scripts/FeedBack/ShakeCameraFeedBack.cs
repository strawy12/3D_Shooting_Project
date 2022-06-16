using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCameraFeedBack :FeedBack
{
     private CinemachineFreeLook _cmFLCam;

    [SerializeField]
    [Range(0f, 5f)] 
    private float _amplitude = 1, _intensity = 1;

    [SerializeField]
    [Range(0f, 1f)]
    private float _duration = 0.1f;

    private List<CinemachineBasicMultiChannelPerlin>_noiseList = new List<CinemachineBasicMultiChannelPerlin>();

    private void Awake()
    {
        if (_cmFLCam == null)
        {
            _cmFLCam = Define.FLCam;
        }

        _noiseList.Add(_cmFLCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
        _noiseList.Add(_cmFLCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
        _noiseList.Add(_cmFLCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
    }

    public override void CompletePrevFeedBack()
    {
        StopAllCoroutines();

        _noiseList.ForEach(x => x.m_AmplitudeGain = 0);
    }

    public override void CreateFeedBack()
    {
        _noiseList.ForEach(x => x.m_AmplitudeGain = _amplitude);
        _noiseList.ForEach(x => x.m_FrequencyGain = _intensity);


        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while(time > 0)
        {
            _noiseList.ForEach(x => x.m_AmplitudeGain = _amplitude);
            _noiseList.ForEach(x => x.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration));
            yield return null;
            time -= Time.deltaTime;
        }

        _noiseList.ForEach(x => x.m_AmplitudeGain = 0f);
    }

}
