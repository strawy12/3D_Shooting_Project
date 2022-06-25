using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTan : PoolableMono
{
    private Transform _parentTrs;
    private Rigidbody _rigid;

    private bool _isThrow;
    [SerializeField] private float _duration;


    public void LateUpdate()
    {
        if(!_isThrow && _parentTrs != null)
        {
            transform.position = _parentTrs.position;
            transform.rotation = _parentTrs.rotation;
        }
    }

    public void InitObject(Transform parent)
    {
        _parentTrs = parent;
        _isThrow = false;
    }

    public void ThrowHoshiTan(Vector3 dir,float force)
    {
        if(_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }
        _isThrow = true;
        _rigid.useGravity = true;
        _rigid.AddForce(dir* force);
        _rigid.AddTorque(dir* force);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_isThrow) return;
        if(other.gameObject.layer == 6)
        {
            Vector3 pos = transform.position;
          HoshiTanEffect effect =  PoolManager.Inst.Pop("HoshiTanEffect") as HoshiTanEffect;
            effect.StartEffect(_duration);
            effect.transform.position = pos;
            PoolManager.Inst.Push(this);
        }
    }

    public override void Reset()
    {
        _isThrow = false;
        _parentTrs = null;
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }

        _rigid.useGravity = false;
    }

}
