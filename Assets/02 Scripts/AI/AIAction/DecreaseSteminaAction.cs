using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSteminaAction : AIAction
{
    [SerializeField] private float _maxSpeed = 7f;

    public override void Enter()
    {
    }

    public override void Execute()
    {
        _aiMovementData.currentStamina -= _aiMovementData.decreaseStamina * Time.deltaTime;
        _aiMovementData.currentStamina = Mathf.Clamp(_aiMovementData.currentStamina, 0f, _aiMovementData.maxStamina);

        float t = _aiMovementData.currentStamina / _aiMovementData.maxStamina;
        float speed = Mathf.Lerp(0f, _maxSpeed, t);

        _aiBrain.SetSpeed(speed);
    }

    public override void Exit()
    {
        _aiBrain.SetSpeed(0f);
    }
}
