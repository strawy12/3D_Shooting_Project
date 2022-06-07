using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private Transform _throwPos;

    [SerializeField] private float _maxThrowForce;
    [SerializeField] private HoshiTan _hoshiTanPref;
    [SerializeField] private float _cooltimeDelay;
    [SerializeField] private float _throwDelay;

    private bool _canThrow;

    private HoshiTan _currentObject;

    public void Throw()
    {
        _hoshiTanPref = Instantiate(_hoshiTanPref, _throwPos.position, _throwPos.rotation);
        _currentObject.transform.SetParent(_throwPos);

        StartCoroutine(ThrowCoroutine());
    }

    private IEnumerator ThrowCoroutine()
    {
        yield return new WaitForSeconds(_throwDelay);

        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        direction.y = 1f;

        _currentObject.transform.SetParent(null);
        _currentObject.ThrowHoshiTan(direction.normalized, _maxThrowForce);

    }
}
