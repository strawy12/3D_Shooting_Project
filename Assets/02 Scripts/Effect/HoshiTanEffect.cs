using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanEffect : PoolableMono
{
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartEffect(float delay)
    {
        gameObject.SetActive(true);

        StartCoroutine(WaitEffectEnd(delay));
    }

    private IEnumerator WaitEffectEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);

    }

    public override void Reset()
    {
    }
}
