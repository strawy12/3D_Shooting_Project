using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDecision : AIDecision
{
    [SerializeField] private float _delayTime;
    private float _timer = 0f;

    public void StateEnter()
    {
        _timer = 0f;
    }

    public override bool MakeADecision()
    {
        Debug.Log(11);
        _timer += Time.deltaTime;

        if(_timer >= _delayTime)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
