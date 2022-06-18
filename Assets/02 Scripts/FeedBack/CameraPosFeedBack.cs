using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraPosFeedBack : FeedBack
{
    private CinemachineFreeLook _flCam;

    [SerializeField] private float _duration;
    [SerializeField] private float _returnDuration;
    [Range(1, 179)] [SerializeField] private float _reachValue = 40;
    [SerializeField] private float _originValue;

    private void Awake()
    {
        _flCam = Define.FLCam;
    }

    public override void CompletePrevFeedBack()
    {
        DOTween.Kill(this, true);
    }

    public override void CreateFeedBack()
    {
        int idx;
        float reachValue = _reachValue;
        if (_flCam.m_YAxis.Value < 0.34f)
        {
            idx = 2;
        }

        else if (_flCam.m_YAxis.Value < 0.67f)
        {
            idx = 1;
        }

        else
        {
            idx = 0;
            reachValue *= -1f;
        }


        Sequence seq = DOTween.Sequence();

        seq.Append(DOTween.To(() => _flCam.m_Orbits[idx].m_Radius,
            value => _flCam.m_Orbits[idx].m_Radius = value,
            reachValue, _duration).SetEase(Ease.OutCubic));

        seq.Append(DOTween.To(() => _flCam.m_Orbits[idx].m_Radius,
            value => _flCam.m_Orbits[idx].m_Radius = value,
            _originValue, _returnDuration));

    }

}