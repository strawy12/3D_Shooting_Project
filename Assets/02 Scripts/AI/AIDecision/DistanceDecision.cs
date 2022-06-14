using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    [Range(0.1f, 30f)] public float distance = 5f;

    public override bool MakeADecision()
    {
        float calc = Vector3.Distance(transform.position, _aiBrain.Target.position);

        if(calc < distance)
        {
            if(_aiActionData.targetSpotted == false)
            {
                _aiActionData.targetSpotted = true;
            }
        }

        else
        {
            _aiActionData.targetSpotted = false;
        }

        return _aiActionData.targetSpotted;
    }
}
