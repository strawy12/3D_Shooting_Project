using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _defaultState;

    private AIState _currentState;

    public UnityEvent<Vector3> OnMovementKeyPress;
    public UnityEvent<float> OnChangeKeyMoveSpeed;
   public UnityEvent OnFireButtonPress;
   public UnityEvent OnFireButtonRelease;

    private bool isChanged;

    [SerializeField] private Transform _target;

    public Transform Target { get => _target; }

    private void Awake()
    {
        _target = Define.PlayerRef;
    }

    void Start()
    {
        ChangeState(_defaultState);
        
    }

    void Update()
    {
        if (isChanged) return;

        if(_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector3.zero);
        }

        _currentState.State_Execute();
    }
    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void Move(Vector3 movementDir, float moveSpeed = 0f)
    {
        OnMovementKeyPress?.Invoke(movementDir);
        OnChangeKeyMoveSpeed?.Invoke(moveSpeed);
    }

    public void ChangeState(AIState aIState)
    {
        isChanged = true;
        _currentState?.State_Exit();

        _currentState = aIState;

        _currentState?.State_Enter();
        isChanged = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
