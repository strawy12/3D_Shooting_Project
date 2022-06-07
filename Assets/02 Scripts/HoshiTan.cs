using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTan : PoolableMono
{
    [SerializeField] private GameObject _hoshiTanEffect;
    [SerializeField] private Transform _parentTrs;
    private Rigidbody _rigid;

    private bool _isThrow;

    public void LateUpdate()
    {
        if(!_isThrow)
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
            pos.y = 0f;
            Instantiate(_hoshiTanEffect, pos, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public override void Reset()
    {
        _rigid.useGravity = false;
    }

}
