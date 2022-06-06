using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovementCtrl : MonoBehaviour
{
    [SerializeField] private float _moveMaxSpeed = 3f;
    [SerializeField] private float _rotateMoveSpeed = 80f;
    [SerializeField] private float _turnSpeed = 80f;
    [SerializeField] private int _maxHp = 100;

    [Header("감속, 가속")]
    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _deAcceleration = 50f;


    private Rigidbody _rigid;
    private Collider _collider;

    private Vector3 _currentDir = Vector3.zero;
    private float _currentVelocity = 3f;

    private void Awake()
    {
        // 게임 출시때 꼭 주석 풀기
        //Cursor.lockState = CursorLockMode.Locked;
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (!IsGround())
        {
            _currentDir.y = 0f;
        }

        else
        {
            _currentDir += Physics.gravity * Time.fixedDeltaTime;
        }

        _rigid.velocity = _currentDir * _currentVelocity;
        ChangeBody();
    }

    public void MovementInput(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            // Define static 으로 만들기
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0f;

            Vector3 right = new Vector3(forward.z, 0f, -forward.x);

            Vector3 targetDir = forward * movementInput.z + right * movementInput.x;



            if (Vector3.Dot(_currentDir, targetDir) < 0)
            {
                _currentVelocity = 0f;
            }

            _currentDir = Vector3.RotateTowards(_currentDir, targetDir, _rotateMoveSpeed * Time.deltaTime, 1000f);
            _currentDir.Normalize();
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }


    private float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0f)
        {
            _currentVelocity += _acceleration * Time.deltaTime;
        }

        else
        {
            _currentVelocity -= _deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0f, _moveMaxSpeed);
    }

    private void ChangeBody()
    {
        if (_currentVelocity > 0f)
        {
            Vector3 newForward = _rigid.velocity;
            newForward.y = 0f;

            transform.forward = Vector3.Lerp(transform.forward, newForward, _turnSpeed * Time.deltaTime);
        }
    }

    bool IsGround()
    {
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = _collider.bounds.size * 0.5f;
        return Physics.OverlapBox(pos, size, Quaternion.identity, 1 << 6).Length > 0;
    }

}
