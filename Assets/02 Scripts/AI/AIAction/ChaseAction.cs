using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseAction : AIAction
{
    public override void Enter()
    {
    }

    public override void Execute()
    {

        Vector3 dir = _aiBrain.Target.position - transform.position;
        dir.y = 0f;
        _aiMovementData.direction = dir.normalized;


        _aiBrain.Move(dir.normalized);
    }

    public override void Exit()
    {

    }

}
