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

    protected bool _isDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    protected virtual void ChildAwake()
    {

    }

    public void PlayAttackAnim()
    {
        if (_isDead) return;

        OnPlayActAnimation?.Invoke();

        _animator.SetTrigger(_hashAttack);
    }

    public void SetWalkAnim(bool isWalk)
    {
        if (_isDead) return;

        _animator.SetBool(_hashWalk, isWalk);
    }

    public void AnimateAgent(float velocity)
    {
        SetWalkAnim(velocity > 0.1f);
    }

    public void PlayDeadAnim()
    {
        if (_isDead) return;

        _isDead = true;


        _animator.SetTrigger(_hashDead);
    }

    public void PlayHitAnim()
    {
        if (_isDead) return;
        _animator.SetTrigger(_hashHit);
    }
}
