using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxStaminaDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return _aiMovementData.currentStamina >= _aiMovementData.maxStamina;
    }
}
