using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    protected AIBrain _aiBrain;

    [SerializeField] private bool _isNegetive = false;

    private void Awake()
    {
        _aiActionData = transform.GetComponentInParent<AIActionData>();
        _aiMovementData = transform.GetComponentInParent<AIMovementData>();
        _aiBrain = transform.GetComponentInParent<AIBrain>();

        ChildAwake();
    }

    protected virtual void ChildAwake() { }
    public abstract bool MakeADecision();
    public bool GetDecicionState()
    {
        return MakeADecision() == !_isNegetive;
    }
}
