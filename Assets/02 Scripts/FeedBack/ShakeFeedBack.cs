using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFeedBack : FeedBack
{
    [SerializeField] private GameObject _objectToShake;
    [SerializeField] private float _duration = 0.2f, _strength = 1f, _randomness = 90f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private bool _snapping = false, _fadeOut = true;
    // snapping : ���ڴ����� ������ �ϴ°�, fadeout : ������ ���� �ڸ��� ���ư���
    public override void CompletePrevFeedBack()
    {
        if (_objectToShake == null) return;

        _objectToShake.transform.DOComplete();
        // ��� Ʈ���� ��� �Ϸ�, �Ϸ� Ʈ�� ���� ��ȯ
    }

    public override void CreateFeedBack()
    {
        CompletePrevFeedBack();
        _objectToShake.transform.DOShakePosition(_duration, _strength, _vibrato, _randomness, _snapping, _fadeOut);
    }
}
