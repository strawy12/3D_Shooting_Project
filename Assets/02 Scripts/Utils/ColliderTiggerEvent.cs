using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTiggerEvent : MonoBehaviour
{
    public Action<Collider> OnEnterCollider;
    public Action<Collider> OnStayCollider;
    public Action<Collider> OnExitCollider;

    private void OnTriggerEnter(Collider other)
    {
        OnEnterCollider?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnStayCollider?.Invoke(other);

    }

    private void OnTriggerExit(Collider other)
    {
        OnExitCollider?.Invoke(other);

    }
}
