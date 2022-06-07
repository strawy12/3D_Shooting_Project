using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementCtrl : MonoBehaviour
{
    [SerializeField] private float _moveMaxSpeed = 3f;
    [SerializeField] private float _runMaxSpeed = 10f;
    [SerializeField] private float _rotateMoveSpeed = 80f;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _turnSpeed = 80f;
    [SerializeField] private int _maxHp = 100;

    [Header("����, ����")]
    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _deAcceleration = 50f;


    private Rigidbody _rigid;
    private Collider _collider;

    private Vector3 _currentDir = Vector3.zero;
    private float _currentVelocity = 3f;

    private bool _isRun = false;

    public UnityEvent<float> OnChangeVelocity;


    private void Awake()
    {
        // ���� ��ö� �� �ּ� Ǯ��
        //Cursor.lockState = CursorLockMode.Locked;
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        OnChangeVelocity?.Invoke(_currentVelocity);


        Vector3 velocity = _currentDir;
        velocity.x *= _currentVelocity;
        velocity.y = _rigid.velocity.y;
        velocity.z *= _currentVelocity;

        _rigid.velocity = velocity;
        ChangeBody();
    }

    public void MovementInput(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            // Define static ���� �����
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0f;

            Vector3 right = new Vector3(forward.z, 0f, -forward.x);

            Vector3 targetDir = forward * movementInput.z + right * movementInput.x;

            Vector3 currentDir = _currentDir;
            currentDir.y = 0f;

            if (Vector3.Dot(currentDir, targetDir) < 0)
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

        return Mathf.Clamp(_currentVelocity, 0f, _isRun ? _runMaxSpeed : _moveMaxSpeed);
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

    public void CameraFrontBody()
    {
        if (_currentVelocity > 0f) return;
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0f;
        transform.forward = forward;
    }

    public void ChangeRunState(bool state)
    {
        _isRun = state;
    }

    public void Jump()
    {
        if(IsGround())
        {
            _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }
    

    bool IsGround()
    {
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = _collider.bounds.size * 0.5f;
        return Physics.OverlapBox(pos, size, Quaternion.identity, 1 << 6).Length > 0;
    }

    private void OnDrawGizmos()
    {
        if(_collider == null) _collider = GetComponent<Collider>();

        Gizmos.color = Color.red;
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = Vector3.one* 0.1f;
        Gizmos.DrawWireCube(pos, size);
    }

}
