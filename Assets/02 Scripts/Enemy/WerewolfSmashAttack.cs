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

    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _damage = damage;
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
            pos.y = 0f;
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
            pos.y = 0f;
            effect.SetPositionAndRotation(pos, transform.rotation);
            effect.StartEffect(0.3f);
        }
    }

    private void GenerateColumn(Vector3 pos)
    {
        Column column = PoolManager.Inst.Pop("Skill_Column") as Column;

        if (column != null)
        {
            column.OnColliderEnter.AddListener(AttackSuccess);
            column.transform.SetPositionAndRotation(pos, transform.rotation);
            column.Aspire(_spawnTime, _disableTime, _effectOffset, _lifeTime);
        }
    }

    private void AttackSuccess(Collider col)
    {

    }


}
