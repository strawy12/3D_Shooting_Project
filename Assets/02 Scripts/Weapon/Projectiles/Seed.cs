using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Projectile
{
    private Rigidbody _rigid;
    protected float _timeToLive;

    private int _enemyLayer;
    private int _obstacleLayer;

    private bool _isDead;

    private void Awake()
    {
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
        _enemyLayer = LayerMask.NameToLayer("Player");
        _isEnemy = true;
    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;

        if (_timeToLive >= _projectileData.lifeTime)
        {
            _isDead = true;
            PoolManager.Inst.Push(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead) return;


        if (other.gameObject.layer == _enemyLayer)
        {
            HitEnemy(other);
        }

        _isDead = true;
        PoolManager.Inst.Push(this);

    }

    private void HitEnemy(Collider col)
    {
        IHittable hittable = col.GetComponent<IHittable>();
        if (hittable != null && hittable.IsEnemy == IsEnemy)
        {
            return; // 아군 피격
        }

        hittable?.GetHit(_projectileData.damage * damageFactor, damagerDealer: gameObject);
    }

    private void HitObstacle(Collider col)
    {
        RaycastHit hit;


        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, 1 << _obstacleLayer))
        {
            Quaternion rot = Quaternion.LookRotation(-transform.forward);
            ImpactEffect effect = PoolManager.Inst.Pop(_projectileData.impactObstanclePrefab.name) as ImpactEffect;
            effect.SetPositionAndRotation(hit.point, rot);
            effect.StartEffect();
        }
    }

    public void ShootSeed(Vector3 force)
    {
        if(_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }
        _rigid.AddForce(force);
    }


    public override void Reset()
    {
        damageFactor = 1;
        _timeToLive = 0;
        _isDead = false;
    }

}
