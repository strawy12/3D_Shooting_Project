using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] protected float _moveYPos;
    [SerializeField] protected float _speed;

    private Vector3 _originPos;

    private void Start()
    {
        _originPos = transform.position;    
    }

    void Update()
    {
        float t = Mathf.Sin(Time.time * _speed) * 0.5f + 0.5f;

        float yPos = Mathf.Lerp(-_moveYPos, _moveYPos, t);

        Vector3 pos = _originPos;
        pos.y += yPos;

        transform.position = pos;
    }
}
