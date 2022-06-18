using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [SerializeField] private List<AIDecision> _decisionList;

    [SerializeField] private AIState _positiveState;
    [SerializeField] private AIState _negetiveState;

    public enum OperatorType { And, Or};
    [SerializeField] private OperatorType _operatorType;

    public List<AIDecision> Decisions { get => _decisionList; }
    public AIState Positive { get => _positiveState; }
    public AIState Negetive { get => _negetiveState; }

    public bool CheckAllDecision()
    {
        foreach(var decision in _decisionList)
        {
            if(_operatorType == OperatorType.Or)
            {
                if (decision.GetDecicionState())
                {
                    return true;
                }
            }

            else
            {
                if (!decision.GetDecicionState())
                {
                    return false;
                }
            }
            

        }

        return _operatorType == OperatorType.And; // 여기까지 왔을때 and라면 true, or 이면 false니
    }
}
