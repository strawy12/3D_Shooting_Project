using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _hashAttack = Animator.StringToHash("Attack");
    private int _hashVelocity = Animator.StringToHash("Velocity");
    private int _hashRun = Animator.StringToHash("Run");
    private int _hashAttackCount = Animator.StringToHash("AttackCount");
    private int _hashThrow = Animator.StringToHash("Throw");

    public UnityEngine.Events.UnityEvent OnPlayActAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    public void PlayAttackAnim(int count)
    {
        OnPlayActAnimation?.Invoke();
        _animator.SetInteger(_hashAttackCount, count);
        _animator.SetTrigger(_hashAttack);
    }

    public void SetVelocity(float velocity) 
    {
        _animator.SetFloat(_hashVelocity, velocity);
    }

    public void SetRun(bool isRun)
    {
        _animator.SetBool(_hashRun, isRun);
    }
    public void PlayThrowAnim()
    {
        OnPlayActAnimation?.Invoke();
        _animator.SetTrigger(_hashThrow);
    }

}
