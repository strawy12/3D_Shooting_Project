using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : PoolableMono
{
    private ParticleSystem _particle;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartEffect(float duration)
    {
        gameObject.SetActive(true);
        if(_particle == null)
        {
            _particle = GetComponentInChildren<ParticleSystem>();  
        }

        _particle.Play();
        StartCoroutine(WaitEffectEnd(duration));
    }

    public void ImmediatelyStop()
    {
        _particle.Stop();
        StopAllCoroutines();
        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);
    }

    private IEnumerator WaitEffectEnd(float duration)
    {
        yield return new WaitForSeconds( duration);

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);

    }

    public override void Reset()
    {
    }

}
