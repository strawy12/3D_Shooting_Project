using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaCollider : MonoBehaviour
{
    private Collider _katanaCollider;

    public UnityEngine.Events.UnityEvent<Collider> OnKatanaEnter;

    public bool IsActive
    {
        get
        {
            if (_katanaCollider == null)
            {
                _katanaCollider = GetComponent<Collider>();
            }

            return _katanaCollider.enabled;
        }
        set
        {
            if (_katanaCollider == null)
            {
                _katanaCollider = GetComponent<Collider>();
            }

            _katanaCollider.enabled = value;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        OnKatanaEnter?.Invoke(other);
    }
}
