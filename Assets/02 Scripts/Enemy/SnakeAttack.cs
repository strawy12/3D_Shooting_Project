using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : EnemyAttack
{
    private int _damage;
    [SerializeField] private ColliderTiggerEvent _attackCol;

    private bool _waitAttack;

    protected override void ChildAwake()
    {
        _attackCol.enabled = false;

        _attackCol.OnEnterCollider += (AttackSuccess);
    }

    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _attackCol.enabled = true;
            _waitAttack = true;
            _damage = damage;
            OnAttackFeedBack?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    protected override void ChildAfterWaitAttack()
    {
        _attackCol.enabled = false;
        _waitAttack = false;
    }

    public void AttackSuccess(Collider col)
    { 
        if ((1 << col.gameObject.layer & _targetLayer) == 0) return;
        if (_waitAttack == false) return;
        _waitAttack = false;

        IHittable hittable = GetTarget().GetComponent<IHittable>();

        hittable?.GetHit(damage: _damage, damagerDealer: gameObject);
        _attackCol.enabled = false;
    }

}
