using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : PoolableMono
{
    private ParticleSystem _particle;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartEffect(float lifeTime)
    {
        gameObject.SetActive(true);
        if (_particle == null)
        {
            _particle = GetComponentInChildren<ParticleSystem>();
        }

        _particle.Play();
        StartCoroutine(WaitEffectEnd(lifeTime));
    }

    private IEnumerator WaitEffectEnd(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);

    }

    public override void Reset()
    {
    }
}
