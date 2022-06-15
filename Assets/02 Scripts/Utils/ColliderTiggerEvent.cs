using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTiggerEvent : MonoBehaviour
{
    public UnityEvent<Collider> OnEnterCollider;
    public UnityEvent<Collider> OnStayCollider;
    public UnityEvent<Collider> OnExitCollider;

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
