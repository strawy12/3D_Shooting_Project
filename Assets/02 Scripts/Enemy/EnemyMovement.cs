using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : AgentMovement
{
    private Vector3 _targetDir;
    protected override Vector3 GetForward()
    {
        return _targetDir;
    }

    public void SetTargetDirection(Vector3 dir)
    {
        _targetDir = dir;
    }

}
