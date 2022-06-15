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

    private bool _isDead = false;

    public bool IsEnemy { get; set; }
    public int Health { get; private set; }
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
        float critical = Random.value;
        bool isCritical = false;

        //if (critical <= GameManager.Inst.criticalChance)
        //{
        //    float ratio = Random.Range(GameManager.Inst.criticalMinDamage, GameManager.Inst.criticalMaxDamage);

        //    damage = Mathf.CeilToInt((float)damage * ratio);

        //    isCritical = true;
        //}

        Health -= damage;

        HitPoint = damagerDealer.transform.position;
        OnGetHit?.Invoke();

        //DamagePopup popup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        //popup.Setup(damage, transform.position + new Vector3(0, 0.5f, 0), isCritical);

        if (Health <= 0f)
        {
            _isDead = true;
            _enemyMovement.StopImmediatelly();
            _enemyMovement.enabled = false;
            OnDie?.Invoke();
        }
    }

    public void PerformAttack()
    {
        _enemyAttack.Attack(damage);
    }
}
