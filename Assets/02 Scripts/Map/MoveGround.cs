using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveGround : MonoBehaviour
{
    [SerializeField] private Transform _meshTrs;
    [SerializeField] private float _duration;
    [SerializeField] private float _offset;


    public void TakeAction()
    {
        transform.DOMove(transform.position + Vector3.back * _offset, _duration);
        _meshTrs.DOShakePosition(_duration);
    }


}
