using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _defaultState;
    [SerializeField] private float _stiffenTime;

    public AIActionData ActionData { get; private set; }

    private AIState _currentState;

    public UnityEvent<Vector3> OnMovementKeyPress;
    public UnityEvent<Vector3> OnChangeBody;
    public UnityEvent<float> OnChangeKeyMoveSpeed;
    public UnityEvent OnFireButtonPress;
    public UnityEvent OnSkillButtonPress;
    public UnityEvent OnFireButtonRelease;
    public UnityEvent OnSkillButtonRelease;

    private bool _isChanged;

    private bool _isStiffen;

    [SerializeField] private Transform _target;

    public Transform Target { get => _target; }

    private void Awake()
    {
        _target = Define.PlayerRef;
        ActionData = GetComponentInChildren<AIActionData>();
    }

    void Start()
    {
        ChangeState(_defaultState);

    }

    void Update()
    {
        if (_isChanged) return;
        if (_isStiffen) return;

        if (_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector3.zero);
        }

        _currentState.State_Execute();
    }
    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void UseSkill()
    {
        OnSkillButtonPress?.Invoke();
    }

    public void Move(Vector3 movementDir)
    {
        OnMovementKeyPress?.Invoke(movementDir);
    }

    public void SetSpeed(float moveSpeed)
    {
        OnChangeKeyMoveSpeed?.Invoke(moveSpeed);
    }

    public void ChangeBody(Vector3 dir)
    {
        OnChangeBody?.Invoke(dir);
    }

    public void ChangeState(AIState aIState)
    {
        _isChanged = true;
        _currentState?.State_Exit();

        _currentState = aIState;

        _currentState?.State_Enter();
        _isChanged = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void StartStiffen()
    {
        StartCoroutine(StiffenCoroutine());
    }

    private IEnumerator StiffenCoroutine()
    {
        _isStiffen = true;
        ChangeState(_currentState);
        yield return new WaitForSeconds(_stiffenTime);
        _isStiffen = false;
    }
}
