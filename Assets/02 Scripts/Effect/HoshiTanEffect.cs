using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanEffect : PoolableMono
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _cycleTime = 1f;
    [SerializeField] private float _duration = 5f;

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
        for(int i = 0; i < (int)(delay / _cycleTime); i++)
        {
            DetectHittable();
            yield return new WaitForSeconds(_cycleTime);
        }

        gameObject.SetActive(false);
        PoolManager.Inst.Push(this);
    }

    private void DetectHittable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        foreach(var hit in hits)
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
