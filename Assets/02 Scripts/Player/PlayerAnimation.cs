using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AgentAnimation
{
    private int _hashRun = Animator.StringToHash("Run");
    private int _hashAttackCount = Animator.StringToHash("AttackCount");
    private int _hashThrow = Animator.StringToHash("Throw");



   public void SetAttackCnt(int cnt)
    {
        _animator.SetInteger(_hashAttackCount, cnt);
        PlayAttackAnim();
    }

    public void SetRun(bool isRun)
    {
        if (_isDead) return;

        _animator.SetBool(_hashRun, isRun);
    }
    public void PlayThrowAnim()
    {
        if (_isDead) return;

        OnPlayActAnimation?.Invoke();
        _animator.SetTrigger(_hashThrow);
    }

}
