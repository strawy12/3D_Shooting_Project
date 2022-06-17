using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeFeedBack : FeedBack
{
    [SerializeField]
    private float _reachValue = 0.5f;

   [SerializeField]
    [Range(0f, 1f)]
    private float _duration = 0.1f;

    [ContextMenu("1dd")]

    public override void CreateFeedBack()
    {
        Time.timeScale = _reachValue;

        DOTween.To(() => Time.timeScale,
            timeScale => Time.timeScale = timeScale,
            1f, _duration).SetUpdate(true );
    }

    [ContextMenu("dd")]
    public override void CompletePrevFeedBack()
    {
        DOTween.Kill(this);

        Time.timeScale = 1f;
    }
}
