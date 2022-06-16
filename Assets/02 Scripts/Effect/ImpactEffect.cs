using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : PoolableMono
{
    private ParticleSystem _particle;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartEffect()
    {
        gameObject.SetActive(true);
        if(_particle == null)
        {
            _particle = GetComponent<ParticleSystem>();  
        }

        _particle.Play();
        StartCoroutine(WaitEffectEnd());
    }

    private IEnumerator WaitEffectEnd()
    {
        yield return new WaitForSeconds(_particle.main.duration);

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);

    }

    public override void Reset()
    {
    }

}
