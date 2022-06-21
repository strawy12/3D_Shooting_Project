using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBrain : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTrs;

    private CinemachineBrain _cinemachineBrain;
    private bool _useCinemachine;

    private float _distance;

    private void Awake()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    public void LateUpdate()
    {
        if (_useCinemachine) return;

        transform.position = _targetTrs.position - (transform.forward * _distance);
    }

    public void ChangeBrain(Vector3 movementInput)
    {
        bool useCinemachine = (movementInput.x != 0f && movementInput.z != 0f) || (movementInput == Vector3.zero);

        _useCinemachine = useCinemachine;
        _cinemachineBrain.enabled = useCinemachine;

        if(useCinemachine == false)
        {
            _distance = Vector3.Distance(transform.position, _targetTrs.position);
        }
    }

}
