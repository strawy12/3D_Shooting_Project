using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingAction : AIAction
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        Vector3 dir = _aiBrain.Target.position - transform.position;

        _aiBrain.ChangeBody(dir.normalized);
    }

    public override void Exit()
    {
    }
}
