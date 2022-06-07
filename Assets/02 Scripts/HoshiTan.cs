using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTan : PoolableMono
{
    [SerializeField] private PoolableMono _hoshiTanEffect;
    private Rigidbody _rigid;
    
    public void ThrowHoshiTan(Vector3 dir,float force)
    {
        _rigid.useGravity = true;
        _rigid.AddForce(dir* force);
        _rigid.AddTorque(dir* force);
    }


    private void OnTriggerEnter(Collider other)
    {
    }

    public override void Reset()
    {
        _rigid.useGravity = false;
    }

}
