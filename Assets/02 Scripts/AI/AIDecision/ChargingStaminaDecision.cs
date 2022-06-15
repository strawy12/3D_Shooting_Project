using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStaminaDecision : AIDecision
{
    [SerializeField] private int _chargingPercent = 100;

    public override bool MakeADecision()
    {
        return _aiMovementData.currentStamina >= _aiMovementData.maxStamina * (_chargingPercent / 100f);
    }
}
