using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [SerializeField] private List<AIDecision> _decisionList;

    [SerializeField] private AIState _positiveState;
    [SerializeField] private AIState _negetiveState;

    public List<AIDecision> Decisions { get => _decisionList; }
    public AIState Positive { get => _positiveState; }
    public AIState Negetive { get => _negetiveState; }

    public bool CheckAllDecision()
    {
        foreach(var decision in _decisionList)
        {
            if(!decision.GetDecicionState())
            {
                return false;
            }

        }

        return true;
    }
}
