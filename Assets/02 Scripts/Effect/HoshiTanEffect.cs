using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoshiTanEffect : PoolableMono
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _cycleTime = 1f;
    [SerializeField] private float _duration = 10f;

    [SerializeField] private HoshiTanEffectSound _effectSound;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartEffect(float delay)
    {
        if (_effectSound == null)
        {
            _effectSound = transform.Find("EffectSound").GetComponent<HoshiTanEffectSound>();
        }
        gameObject.SetActive(true);

        _effectSound.PlayExplosionSound();
        StartCoroutine(WaitEffectEnd(delay));
    }

    private IEnumerator WaitEffectEnd(float delay)
    {
        for (int i = 0; i < (int)(delay / _cycleTime); i++)
        {
            yield return new WaitForSeconds(_cycleTime);
            _effectSound.PlayBuffSound();
            DetectHittable();
        }

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);
    }

    private void DetectHittable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        foreach (var hit in hits)
        {
            IHittable hittable = hit.GetComponentInParent<IHittable>();
            if (hittable == null) continue;
            hittable.GetHoshiTanEffect(_duration);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public override void Reset()
    {
    }
}
