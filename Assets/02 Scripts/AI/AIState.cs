using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AIState : MonoBehaviour
{
    [SerializeField] private AIAction _currentAction;
    [SerializeField] private List<AIAction> _executeActionList;

    [SerializeField] private List<AITransition> _currentTransitionList;

    private AIBrain _aiBrain;

    private void Awake()
    {
        _aiBrain = transform.GetComponentInParent<AIBrain>();
    }


    public void State_Enter()
    {
        _currentAction?.Enter();
    }

    public void State_Execute()
    {

        _currentAction?.Execute();

        foreach (AIAction action in _executeActionList)
        {
            action.Execute();
        }

        CheckTransition();
    }

    public void State_Exit()
    {
        _currentAction?.Exit();
    }

    private void CheckTransition()
    {
        foreach (AITransition transition in _currentTransitionList)
        {
            if (transition.CheckAllDecision())
            {
                _aiBrain.ChangeState(transition.Positive);
                break;
            }

            else
            {
                if(transition.Negetive != null)
                {
                    _aiBrain.ChangeState(transition.Negetive);

                }
            }
        }
    }
}
