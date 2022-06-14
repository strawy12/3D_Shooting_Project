using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAction : AIAction
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        _aiMovementData.currentStamina += _aiMovementData.increaseStamina * Time.deltaTime;

        _aiMovementData.currentStamina = Mathf.Clamp(_aiMovementData.currentStamina, 0f, _aiMovementData.maxStamina);
    }

    public override void Exit()
    {
    }
}
