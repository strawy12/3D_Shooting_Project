using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : AgentMovement
{
    private Transform _target;
    private AIActionData _aiActionData;
    private NavMeshAgent _navMeshAgent;
    protected override void ChildAwake()
    {
        _target = Define.PlayerRef;
        _aiActionData = GetComponentInChildren<AIActionData>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void ChildUpdate()
    {
        if(_aiActionData.haveTargetPos)
        {
            if (_navMeshAgent.velocity.sqrMagnitude >= 0.2f * 0.2f && _navMeshAgent.remainingDistance <= 0.5f)
            {
                _aiActionData.arrived = true;
            }
        }

        _navMeshAgent.speed = _currentVelocity;
        _navMeshAgent.SetDestination(transform.position + _currentDir);
    }

    public override void ImmediatelyForwardBody()
    {
        if(_navMeshAgent.velocity != Vector3.zero)
        {
            _navMeshAgent.isStopped = true;
        }

        transform.forward = (_target.position - transform.position).normalized;
    }

    public override void MovementInput(Vector3 movementInput)
    {
        if(movementInput == Vector3.zero)
        {
            _navMeshAgent.isStopped = true;
        }

        else
        {
            _navMeshAgent.isStopped = false;
        }

        base.MovementInput(movementInput);
    }

    protected override Vector3 GetForward()
    {
        if(_aiActionData.targetSpotted)
        {
            return (_target.position - transform.position).normalized;
        }

        return transform.forward;
    }
}
