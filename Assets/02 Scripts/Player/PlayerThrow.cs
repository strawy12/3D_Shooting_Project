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

    public void Throw(float t)
    {
        _currentObject = Instantiate(_hoshiTanPref);
        _currentObject.InitObject(_throwPos);

        StartCoroutine(ThrowCoroutine(t));
    }

    private IEnumerator ThrowCoroutine(float t)
    {
        yield return new WaitForSeconds(_throwDelay);

        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        direction.y = 1f;

        float force = Mathf.Lerp(0f, _maxThrowForce, t);
        _currentObject.ThrowHoshiTan(direction.normalized, force);

    }
}
