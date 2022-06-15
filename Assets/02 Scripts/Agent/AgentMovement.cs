using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AgentMovement : MonoBehaviour
{
    [SerializeField] protected AgentMovementDataSO _movementData;

    protected float _originRunSpeed;
    protected float _originWalkSpeed;



    protected Vector3 _currentDir = Vector3.zero;
    protected float _currentVelocity = 3f;

    protected bool _isRun = false;
    protected bool _isStop = false;

    public UnityEvent<float> OnChangeVelocity;


    private void Awake()
    {

        _originRunSpeed = _movementData.runMaxSpeed;
        _originWalkSpeed = _movementData.moveMaxSpeed;
        ChildAwake();
    }
    protected virtual void ChildAwake() { }

    private void FixedUpdate()
    {
        if (_isStop) return;

        OnChangeVelocity?.Invoke(_currentVelocity);
        ChildUpdate();
    }

    protected virtual void ChildUpdate() { }

    public virtual void MovementInput(Vector3 movementInput)
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
    protected float CalculateSpeed(Vector3 movementInput)
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


    public abstract void ImmediatelyForwardBody();
    protected abstract Vector3 GetForward();


    public void ChangeRunState(bool state)
    {
        _isRun = state;
    }

    public void ChangeMoveSpeed(float speed)
    {
        if (speed <= 0f) return;

        _movementData.moveMaxSpeed = speed;
        _movementData.runMaxSpeed = speed * 1.5f;
    }

    public void ResetMoveSpeed()
    {
        _movementData.runMaxSpeed = _originRunSpeed;
        _movementData.moveMaxSpeed = _originWalkSpeed;
    }

    public void StopImmediatelly()
    {
        _isStop = true;
        _currentVelocity = 0f;
        _currentDir = Vector3.zero;
    }

}
