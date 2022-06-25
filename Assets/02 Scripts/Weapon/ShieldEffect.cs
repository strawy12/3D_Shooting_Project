using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : PoolableMono
{

    public void StartEffect()
    {
        gameObject.SetActive(true);
    }

    public void EndEffect()
    {
        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);

    }

    public override void Reset()
    {

    }

}
