using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        _aiMovementData.direction = Vector3.zero;
        _aiBrain.Move(Vector3.zero);
    }

    public override void Exit()
    {
    }
}
