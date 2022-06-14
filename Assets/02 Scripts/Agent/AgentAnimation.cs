using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    protected Animator _animator;

    protected int _hashAttack = Animator.StringToHash("Attack");
    protected int _hashWalk = Animator.StringToHash("Walk");
    protected int _hashDead = Animator.StringToHash("Dead");
    protected int _hashHit = Animator.StringToHash("Hit");

    public UnityEngine.Events.UnityEvent OnPlayActAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    protected virtual void ChildAwake()
    {
    }

    public void PlayAttackAnim()
    {
        OnPlayActAnimation?.Invoke();

        _animator.SetTrigger(_hashAttack);
    }

    public void SetWalkAnim(bool isWalk)
    {
        _animator.SetBool(_hashWalk, isWalk);
    }

    public void AnimateAgent(float velocity)
    {
        SetWalkAnim(velocity > 0f);
    }

    public void PlayDeadAnim()
    {
        _animator.SetTrigger(_hashDead);
    }

    public void PlayHitAnim()
    {
        _animator.SetTrigger(_hashHit);
    }
}
