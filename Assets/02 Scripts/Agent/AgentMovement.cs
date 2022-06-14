using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AgentMovement : MonoBehaviour
{
    [SerializeField] protected AgentMovementDataSO _movementData;

    protected Rigidbody _rigid;
    protected Collider _collider;

    protected Vector3 _currentDir = Vector3.zero;
    protected float _currentVelocity = 3f;

    protected bool _isRun = false;

    public UnityEvent<float> OnChangeVelocity;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    protected virtual void ChildAwake()
    {

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
            // Define static 으로 만들기
            Vector3 forward = GetForward();
            forward.y = 0f;

            Vector3 right = new Vector3(forward.z, 0f, -forward.x);

            Vector3 targetDir = forward * movementInput.z + right * movementInput.x;

            Vector3 currentDir = _currentDir;
            currentDir.y = 0f;

            if (Vector3.Dot(currentDir, targetDir) < 0)
            {
                _currentVelocity = 0f;
            }

            _currentDir = Vector3.RotateTowards(_currentDir, targetDir, _movementData.rotateMoveSpeed * Time.deltaTime, 1000f);
            _currentDir.Normalize();
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    protected abstract Vector3 GetForward();

    private float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0f)
        {
            _currentVelocity += _movementData.acceleration * Time.deltaTime;
        }

        else
        {
            _currentVelocity -= _movementData.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0f, _isRun ? _movementData.runMaxSpeed : _movementData.moveMaxSpeed);
    }

    private void ChangeBody()
    {
        if (_currentVelocity > 0f)
        {
            Vector3 newForward = _rigid.velocity;
            newForward.y = 0f;

            transform.forward = Vector3.Lerp(transform.forward, newForward, _movementData.turnSpeed * Time.deltaTime);
        }
    }

    public void ImmediatelyForwardBody()
    {
        if (_currentVelocity > 0f) return;
        Vector3 forward = GetForward();
        forward.y = 0f;
        transform.forward = forward;
    }

    public void ChangeRunState(bool state)
    {
        _isRun = state;
    }


    protected bool IsGround()
    {
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = _collider.bounds.size * 0.5f;
        return Physics.OverlapBox(pos, size, Quaternion.identity).Length > 1;
    }

}
