using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : AIAction
{
    private Vector3 _targetPos;
    private Vector3 _movementDir;
    private float _distance;
    [SerializeField] private float _minDistance = 2f; 
    [SerializeField] private float _maxDistance = 5f; 
    public override void Enter()
    {
        while(true)
        {
            _movementDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            _distance = Random.Range(_minDistance, _maxDistance);
            _movementDir.Normalize();

            if (!Physics.Raycast(transform.position, _movementDir, _distance, ~(1 << 6)))
            {
                break;
            }
        }

        _targetPos = transform.position + (_movementDir * _distance);

    }

    public override void Execute()
    {
        if (_aiActionData.arrived) return;

        if(Vector3.Distance(transform.position, _targetPos) <= 1f)
        {
            _aiActionData.arrived = true;
        }

        else
        {
            _aiMovementData.direction = _movementDir;
            _aiBrain.Move(_movementDir);
        }
    }

    public override void Exit()
    {
        _aiActionData.arrived = false;
    }

}
