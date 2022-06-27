using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WerewolfSmashAttack : EnemyAttack
{
    [SerializeField] private float _delayTime;

    [SerializeField] private float _generateDelayTime;
    [SerializeField] private float _generatePosOffset;
    [SerializeField] private float _generateInterval;

    [Header("Column Info")]
    [SerializeField] private float _disableTime;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _effectOffset;
    [SerializeField] private float _lifeTime;

    private int _damage;
    [SerializeField ]private float _damageFactor = 2f;
    public bool WaitAttack => _waitBeforeNextAttack;

    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _damage = (int)(damage * _damageFactor);
            OnAttackFeedBack?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
            StartCoroutine(WaitDelayTime());
        }
    }

    private IEnumerator WaitDelayTime()
    {
        yield return new WaitForSeconds(_delayTime);
        GenerateDustEffect();


        for (int i = 1; i <= 3; i++)
        {
            Vector3 pos = transform.position + (transform.forward * _generatePosOffset) + (transform.forward * i * _generateInterval);
            GenerateColumn(pos);
            yield return new WaitForSeconds(_generateDelayTime);
        }

    }

    private void GenerateDustEffect()
    {
        DustEffect effect = PoolManager.Inst.Pop("DustEffect") as DustEffect;
        if (effect != null)
        {
            Vector3 pos = transform.position;
            effect.SetPositionAndRotation(pos, transform.rotation);
            effect.StartEffect(0.3f);
        }
    }

    private void GenerateColumn(Vector3 pos)
    {
        Column column = PoolManager.Inst.Pop("Skill_Column") as Column;
        Debug.Log(pos);
        if (column != null)
        {
            column.OnColliderEnter.RemoveAllListeners();
            column.OnColliderEnter.AddListener(AttackSuccess);
            column.transform.SetPositionAndRotation(pos, transform.rotation);
            column.Aspire(_spawnTime, _disableTime, _effectOffset, _lifeTime);
        }
    }

    private void AttackSuccess(Collider col)
    {
        if (((1 << col.gameObject.layer) & _targetLayer) == 0) return;

        _aiBrain.ActionData.attack = false;
        IHittable hittable = GetTarget().GetComponent<IHittable>();
        hittable.HitPoint = col.transform.position;
        hittable?.GetHit(damage: _damage, damagerDealer: gameObject);
    }


}
