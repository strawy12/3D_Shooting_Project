using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImageEffect : PoolableMono
{
    private Animator _animator;
    private Material _material;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (_material == null)
        {
            _material = GetComponentInChildren<SkinnedMeshRenderer>().material;

            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var renderer in renderers)
            {
                renderer.material = _material;
            }

        }
    }

    public void SetModelAnim(Animator originAnimator, float lifeTime)
    {
        Init();

        int nowPlayStateHash = originAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        float nomalizedTime = originAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _animator.Play(nowPlayStateHash, 0, nomalizedTime);

        foreach (var param in originAnimator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Float:
                    _animator.SetFloat(param.name, originAnimator.GetFloat(param.name));
                    break;

                case AnimatorControllerParameterType.Int:
                    _animator.SetInteger(param.name, originAnimator.GetInteger(param.name));
                    break;

                case AnimatorControllerParameterType.Bool:
                    _animator.SetBool(param.name, originAnimator.GetBool(param.name));
                    break;
            }
        }
        _animator.speed = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(_material.DOFade(0f, lifeTime));
         
        seq.AppendCallback(() =>
        {
            Reset();
            PoolManager.Inst.Push(this);
        });
    }


    public override void Reset()
    {
        if (_material == null) return;

        _material.color = Color.white;
    }
}
