using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectiesPref;
    [SerializeField] private Transform _firePos;

    [SerializeField] private float _attackDelay = 0.75f;

    private int _attackStackCnt = 0;
    private bool _isAttack = false;
    private bool _isKeyPress = false;

    public UnityEvent<int> OnAttack;
    public UnityEvent OnAttackEnd;

    public void StartAttack()
    {
        if (_isAttack) return;
        if (_isKeyPress) return;

        _isKeyPress = true;
        _isAttack = true;
        _attackStackCnt++;

        if(_attackStackCnt > 3)
        {
            _attackStackCnt = 1;
        }
        //GameObject bullet = Instantiate(_bulletPref, _firePos);
        //bullet.transform.SetParent(null);

        OnAttack?.Invoke(_attackStackCnt);
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        if(_isAttack)
        {
            _isAttack = false;
            OnAttackEnd?.Invoke();
        }

        yield return new WaitForSeconds(0.5f);

        if(!_isAttack)
        {
            _attackStackCnt = 0;
        }
    }

    public void AttackKeyUp()
    {
        _isKeyPress = false;
    }


}

