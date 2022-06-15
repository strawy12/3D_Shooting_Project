using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : PoolableMono
{
    [SerializeField] private ProjectileDataSO _projectileData;

    protected bool _isEnemy;

    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }

    public int damageFactor = 1;

    public virtual ProjectileDataSO ProjectileData
    {
        get => _projectileData;
        set
        {
            _projectileData = value;
            damageFactor = _projectileData.damage;
        }
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

}
