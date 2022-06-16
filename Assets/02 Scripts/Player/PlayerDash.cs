using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private float _dashCooltime;
    [SerializeField] private float _dashSpeed;

    private bool _isWaiting;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();

    }

    public void StartDash()
    {
        if (_isWaiting) return;
        _isWaiting = true;

        _rigid.AddForce(transform.forward * _dashSpeed);
        StartCoroutine(WatingCoolTime());
    }

    private IEnumerator WatingCoolTime()
    {
        yield return new WaitForSeconds(_dashCooltime);
        _isWaiting = false;
    }
}
