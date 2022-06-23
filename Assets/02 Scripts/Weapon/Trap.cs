using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : PoolableMono
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _duration;


    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & _targetMask) != 0)
        {
            Enemy hit = other.GetComponentInParent<Enemy>();
            if (hit == null) return;

            hit.GetHitTrapEffect(_duration);
            PoolManager.Inst.Push(this);
        }
    }

    public override void Reset()
    {
    }
}
