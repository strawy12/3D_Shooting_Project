using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ItemObject : PoolableMono
{
    [SerializeField] protected float _moveYPos;
    [SerializeField] protected float _speed;
    [SerializeField] private float _rotateSpped;

    private Vector3 _originPos;

    public UnityEvent OnTriggerEnter;

    private void Start()
    {
        _originPos = transform.position;
    }

    protected virtual void Update()
    {
        float t = Mathf.Sin(Time.time * _speed) * 0.5f + 0.5f;

        float yPos = Mathf.Lerp(-_moveYPos, _moveYPos, t);

        Vector3 pos = _originPos;
        pos.y += yPos;

        transform.position = pos;

        Vector3 angle = transform.eulerAngles;
        angle.y += Time.deltaTime * _rotateSpped;

        transform.rotation = Quaternion.Euler(angle);
    }


}
