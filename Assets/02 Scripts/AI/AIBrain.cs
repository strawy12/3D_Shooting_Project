using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _defaultState;
    [SerializeField] private string _animalName;

    private AIState _currentState;

    public UnityEvent<Vector3> OnMovementKeyPress;
   public UnityEvent<Vector3> OnPointerPositionChanged;
   public UnityEvent OnFireButtonPress;
   public UnityEvent OnFireButtonRelease;

    private bool isChanged;

    [SerializeField] private Transform _target;

    public Transform Target { get => _target; }

    private void Awake()
    {
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

    public void Move(Vector3 movementDir, Vector3 targetDir)
    {
        OnMovementKeyPress?.Invoke(movementDir);
        OnPointerPositionChanged?.Invoke(targetDir);
    }

    public void ChangeState(AIState aIState)
    {
        isChanged = true;
        _currentState?.State_Exit();

        _currentState = aIState;

        _currentState.State_Enter();
        isChanged = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
