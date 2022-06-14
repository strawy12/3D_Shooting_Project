using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    //임시 코드 에너미 데이터 만들어야함
    [SerializeField] private int damage;
    public bool IsEnemy { get; set; }

    public Vector3 HitPoint { get; set; }

    public UnityEvent OnDie;
    public UnityEvent OnGetHit;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public void GetHit(int damage, GameObject damagerDealer)
    {
    }

    public void PerformAttack()
    {
        _enemyAttack.Attack(damage);
    }
}
