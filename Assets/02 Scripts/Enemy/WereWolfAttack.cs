using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfAttack : EnemyAttack
{
    private int _damage;
    private bool _waitAttack;
    // TODO : ���� �ڵ带 �����ϰ� �ȴٸ� Snake ���ð� �Բ� �� collider trigger event ���� ������ �����ϱ� �䱸��
    [SerializeField] private ColliderTiggerEvent _attackCol;
    [SerializeField] private WerewolfSmashAttack _smashAttack;

    protected override void ChildAwake()
    {
        _attackCol.enabled = false;
        _smashAttack = GetComponent<WerewolfSmashAttack>();

        _attackCol.OnEnterCollider += (AttackSuccess);
    }
    public override void Attack(int damage)
    {
        if (_smashAttack.WaitAttack == false && _waitBeforeNextAttack == false)
        {
            _smashAttack.Attack(damage);
            StartCoroutine(WaitBeforeAttackCoroutine());
            return;
        }

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
        if (((1 << col.gameObject.layer) & _targetLayer) == 0) return;

        if (_waitAttack == false) return;
        _waitAttack = false;

        _aiBrain.ActionData.attack = false;
        IHittable hittable = GetTarget().GetComponent<IHittable>();
        hittable.HitPoint = _attackCol.transform.position;
        hittable?.GetHit(damage: _damage, damagerDealer: gameObject);
        _attackCol.enabled = false;

    }

}
