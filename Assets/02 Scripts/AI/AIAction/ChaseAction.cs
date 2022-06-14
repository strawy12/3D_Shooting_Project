using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseAction : AIAction
{
    [SerializeField] private float _chaseMaxSpeed = 7f;



    public override void Enter()
    {
    }

    public override void Execute()
    {
        _aiMovementData.currentStamina -= _aiMovementData.decreaseStamina * Time.deltaTime;
        _aiMovementData.currentStamina = Mathf.Clamp(_aiMovementData.currentStamina, 0f, _aiMovementData.maxStamina);

        Vector3 dir = _aiBrain.Target.position - transform.position;

        _aiMovementData.direction = dir.normalized;

        float t = _aiMovementData.currentStamina / _aiMovementData.maxStamina;
        float speed = Mathf.Lerp(0f, _chaseMaxSpeed, t);
        _aiBrain.Move(dir.normalized, speed);
    }

    public override void Exit()
    {

    }

}
