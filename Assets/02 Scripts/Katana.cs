using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    [SerializeField] private KatanaCollider _katanaCollider;
    [SerializeField] private TrailRenderer _trailRenderer;

    public void KatanaEnter(Collider other)
    {
        Debug.Log("Attack");
    }

    public void AttackStart()
    {
        _trailRenderer.enabled = true;
    }

    public void AttackEnd()
    {
        _trailRenderer.enabled = false;
    }
}
