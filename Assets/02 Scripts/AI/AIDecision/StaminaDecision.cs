using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaDecision : AIDecision
{
    [Header("stamina <= _value")]
    [SerializeField] private float _value = 0f;

    public override bool MakeADecision()
    {
        return _aiMovementData.currentStamina <= _value;
    }
}
