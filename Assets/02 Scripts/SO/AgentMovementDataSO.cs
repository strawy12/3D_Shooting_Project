using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/MovementData")]
public class AgentMovementDataSO : ScriptableObject
{
    public float moveMaxSpeed = 3f;
    public float runMaxSpeed = 10f;
    public float rotateMoveSpeed = 80f;
    public float turnSpeed = 80f;

    [Header("감속, 가속")]
    public float acceleration = 50f;
    public float deAcceleration = 50f;
}
