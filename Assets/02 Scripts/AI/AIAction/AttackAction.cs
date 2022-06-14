using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        _aiMovementData.direction = Vector3.zero;
        _aiMovementData.pointOfIntgerest = _aiBrain.Target.position;
        _aiBrain.Move(_aiMovementData.direction);
        _aiActionData.attack = true;

        _aiBrain.Attack();
    }

    public override void Exit()
    {
    }
}
