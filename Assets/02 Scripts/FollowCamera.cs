using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RangeValue
{
    public float min;
    public float max;
}

public class FollowCamera : MonoBehaviour
{
    public Transform _targetTransform;

    [Range(2.0f, 20.0f)]
    public float _distance = 10f;

    [Range(2.0f, 20.0f)]
    public float _height = 2.0f;

    public float _moveDamping = 15f;
    public float _rotateDamping = 10f;

    public float _targetOffset = 2f;

    void LateUpdate()
    {
        Vector3 pos = _targetTransform.position + (-_targetTransform.forward * _distance) + (_targetTransform.up * _height);
        transform.position = Vector3.Slerp(transform.position, pos, _moveDamping * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetTransform.rotation, _rotateDamping * Time.deltaTime);

        transform.LookAt(_targetTransform.position + (_targetTransform.up * _targetOffset));
    }
}
