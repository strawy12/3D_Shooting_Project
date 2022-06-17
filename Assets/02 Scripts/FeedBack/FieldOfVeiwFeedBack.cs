using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class FieldOfVeiwFeedBack : FeedBack
{
    private CinemachineFreeLook _flCam;

    [SerializeField] private float _duration;
    [SerializeField] private float _returnDuration;
    [Range(1, 179)] [SerializeField] private int _reachValue = 40;

    private int _originValue;

    private void Awake()
    {
        _flCam = Define.FLCam;
        _originValue = (int)_flCam.m_Lens.FieldOfView;
    }

    public override void CompletePrevFeedBack()
    {
        DOTween.Kill(this);
        _flCam.m_Lens.FieldOfView = _originValue;
    }

    public override void CreateFeedBack()
    {
        _originValue = (int)_flCam.m_Lens.FieldOfView;

        Sequence seq = DOTween.Sequence();

        seq.Append(DOTween.To(() => _flCam.m_Lens.FieldOfView,
            value => _flCam.m_Lens.FieldOfView = value,
            _reachValue, _duration).SetEase(Ease.OutCubic));

        seq.Append(DOTween.To(() => _flCam.m_Lens.FieldOfView,
            value => _flCam.m_Lens.FieldOfView = value,
            _originValue, _returnDuration));
    }

}