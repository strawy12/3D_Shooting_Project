using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomAttack : EnemyAttack
{
    [SerializeField] private Transform[] _firePos;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _shootForce;

    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            OnAttackFeedBack?.Invoke();
            StartCoroutine(AttackCoroutine(damage));
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine(int damage)
    {
        yield return new WaitForSeconds(_attackDelay);

        GenerateSeed(_firePos[0].position, damage);
        GenerateSeed(_firePos[1].position, damage);

    }

    private void GenerateSeed(Vector3 pos, int damage)
    {
        Seed seed = PoolManager.Inst.Pop("Seed") as Seed;
        seed.SetPositionAndRotation(pos, Quaternion.identity);
        seed.damageFactor = damage;
        seed.ShootSeed(pos + transform.forward * _shootForce);


    }
}
