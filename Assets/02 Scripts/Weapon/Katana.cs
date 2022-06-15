using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    // 무기 데이터
    [SerializeField] private int _damage;
    [SerializeField] private KatanaCollider _katanaCollider;
    [SerializeField] private ParticleSystem _trailParticle;

    public void KatanaEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.GetMask("Enemy"))
        other.GetComponent<IHittable>()?.GetHit(_damage, gameObject);
    }

    public void AttackStart()
    {
        _trailParticle.Play();
    }

}
