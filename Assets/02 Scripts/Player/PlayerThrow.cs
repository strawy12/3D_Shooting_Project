using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private Transform _throwPos;

    [SerializeField] private float _throwForce;
    [SerializeField] private float _cooltimeDelay;
    [SerializeField] private float _throwDelay;
    [SerializeField] private float _chargingSpeed = 2f;

    public UnityEvent ThrowFeedback;
    public UnityEvent EndThrow;
    private bool _canThrow = true;

    private HoshiTan _currentObject;


    public void Throw()
    {
        if (!CanThrow()) return;
        _canThrow = false;
        _currentObject = PoolManager.Inst.Pop("HoshiTan") as HoshiTan;
        _currentObject.InitObject(_throwPos);

        ThrowFeedback?.Invoke();
        StartCoroutine(ThrowCoroutine());
    }

    public bool CanThrow()
    {
        return _canThrow && GameManager.Inst.Data.GetItemCount(EItemType.HoshiTan) != 0;
    }

    private IEnumerator ThrowCoroutine()
    {
        yield return new WaitForSeconds(_throwDelay);

        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        direction.y = 0f;

        _currentObject.ThrowHoshiTan(direction.normalized, _throwForce);

        EndThrow?.Invoke();
        GameManager.Inst.SubItemCount(EItemType.HoshiTan);

        StartCoroutine(CooltimeDelay());
    }

    private IEnumerator CooltimeDelay()
    {
        yield return new WaitForSeconds(_cooltimeDelay);
        _canThrow = true;
    }
}
