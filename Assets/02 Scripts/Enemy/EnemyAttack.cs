using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _aiBrain;
    protected Enemy _enemy;

    public float attackDelay = 1f;

    protected bool _waitBeforeNextAttack;

    public bool WaitingForNextAttack => _waitBeforeNextAttack;

    protected bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    public UnityEvent AttackFeedBack;

    private void Awake()
    {
        _aiBrain = GetComponent<AIBrain>();
        _enemy = GetComponent<Enemy>();
        ChildAwake();
    }

    protected virtual void ChildAwake() { }

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeforeNextAttack = false;
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
