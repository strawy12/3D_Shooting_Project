using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private BulletDataSO _bulletData;

    protected bool _isEnemy;

    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }

    public int damageFactor = 1;

    public virtual BulletDataSO BulletData
    {
        get => _bulletData;
        set
        {
            _bulletData = value;
            damageFactor = _bulletData.damage;
        }
    }

    public void Update()
    {
        transform.Translate(Vector3.forward * 10f * Time.deltaTime);
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
