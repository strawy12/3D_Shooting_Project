using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void Enter()
    {
        _aiActionData.attack = true;
    }

    public override void Execute()
    {
        _aiMovementData.direction = Vector3.zero;
        _aiBrain.Move(_aiMovementData.direction);

        _aiBrain.Attack();
    }

    public override void Exit()
    {
        _aiActionData.attack = false;
    }
}
