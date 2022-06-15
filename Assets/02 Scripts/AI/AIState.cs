using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class AIState : MonoBehaviour
{
    public UnityEvent ActionEnter;
    public UnityEvent ActionExecute;
    public UnityEvent ActionExit;

    [SerializeField] private List<AITransition> _currentTransitionList;

    private AIBrain _aiBrain;

    private void Awake()
    {
        _aiBrain = transform.GetComponentInParent<AIBrain>();
    }


    public void State_Enter()
    {
        ActionEnter?.Invoke();
    }

    public void State_Execute()
    {
        ActionExecute?.Invoke();

        CheckTransition();
    }

    public void State_Exit()
    {
        ActionExit?.Invoke();
    }

    private void CheckTransition()
    {
        foreach (AITransition transition in _currentTransitionList)
        {
            if (transition == null) return;

            if (transition.CheckAllDecision())
            {
                if (transition.Positive != null)
                {
                    _aiBrain.ChangeState(transition.Positive);
                    break;
                }
            }
            else
            {

                if (transition.Negetive != null)
                {

                    _aiBrain.ChangeState(transition.Negetive);
                    break;
                }
            }

        }
           
    }
}
