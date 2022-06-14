using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    protected AIBrain _aiBrain;

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    private void Awake()
    {
        _aiActionData = transform.GetComponentInParent<AIActionData>();
        _aiMovementData = transform.GetComponentInParent<AIMovementData>();
        _aiBrain = transform.GetComponentInParent<AIBrain>();

        ChildAwake();
    }

    protected virtual void ChildAwake() { }

    protected IEnumerator DalayFunc(System.Action func, float delay)
    {

        yield return new WaitForSeconds(delay);

        func.Invoke();
    }

    protected IEnumerator DalayFunc<T>(System.Action<T> func, float delay, T param)
    {
        yield return new WaitForSeconds(delay);

        func.Invoke(param);
    }


}
