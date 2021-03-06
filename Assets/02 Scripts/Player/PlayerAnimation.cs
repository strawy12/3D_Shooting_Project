using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AgentAnimation
{
    private int _hashRun = Animator.StringToHash("Run");
    private int _hashAttackCount = Animator.StringToHash("AttackCount");
    private int _hashThrow = Animator.StringToHash("Throw");
    private int _hashCrouch = Animator.StringToHash("Crouch");



   public void SetAttackCnt(int cnt)
    {
        _animator.SetInteger(_hashAttackCount, cnt);
        PlayAttackAnim();
    }   

    public void SetCrouchAnim(float rootMotionDelay)
    {
        _animator.applyRootMotion = true;
        _animator.SetTrigger(_hashCrouch);

        StartCoroutine(RootMotionDelay(rootMotionDelay));
    }

    private IEnumerator RootMotionDelay(float rootMotionDelay)
    {
        yield return new WaitForSeconds(rootMotionDelay);
        _animator.applyRootMotion = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
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
