using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _firePos;
    [Header("�Ѿ� �ִ� ���� ����")]
    [SerializeField] private int _maxAmmo = 20;
    [Header("�Ѿ� �� ����")]
    [SerializeField] private int _ammoCapacity = 200;
    [Header("���� �Ѿ�")]
    [SerializeField] private bool _infinityBullet;

    private int _currentAmmo = 0;

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;

    public void FireBullet()
    {
        if(_currentAmmo < 0)
        {
            OnShootNoAmmo?.Invoke();
            return;
        }

        GameObject bullet = Instantiate(_bulletPref, _firePos);
        bullet.transform.SetParent(null);
    }


}

