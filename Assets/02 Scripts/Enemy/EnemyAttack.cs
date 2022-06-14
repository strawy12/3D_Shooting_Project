using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _aiBrain;
    // Enemy 스크립트

    public float attackDelay = 1f;

    protected bool _waitBeeforeNextAttack;

    public bool WaitingForNextAttack => _waitBeeforeNextAttack;

    protected bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    public UnityEvent AttackFeedBack;

    private void Awake()
    {
        _aiBrain = GetComponent<AIBrain>();
        ChildAwake();
    }

    protected virtual void ChildAwake() { }

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeeforeNextAttack = false;
    }

    public void Reset()
    {
        StartCoroutine(WaitBeforeAttackCoroutine());
    }

    public Transform GetTarget()
    {
        return _aiBrain.Target;
    }

    public abstract void Attack(int damage);
}
