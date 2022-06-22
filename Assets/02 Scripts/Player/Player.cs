using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHittable
{
    [SerializeField]
    private int _maxHealth;

    private int _health;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
        }
    }

    private bool _isDead = false;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    public Vector3 HitPoint { get; set; }

    //피아 식별용    
    public bool IsEnemy => false;

    public int HitCount { get; set; }

    public bool CanAttack { get; set; }

    [SerializeField] private Transform _hitEffectPos;

    //넉백 처리를 위한 에이전트 무브먼트 가져오기
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Health = _maxHealth;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        Debug.Log("dd");
        Health -= damage;
        OnGetHit?.Invoke();
        GenerateHitEffect();
        if (Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }
    }
    public void GenerateHitEffect()
    {
        ImpactEffect hitEffect = PoolManager.Inst.Pop("HitEffect_1") as ImpactEffect;
        hitEffect.SetPositionAndRotation(_hitEffectPos.position, Quaternion.LookRotation(transform.forward));
        hitEffect.StartEffect();
    }

    public void KnockBack(Vector3 direction, float power, float duration)
    {
        _playerMovement.KnockBack(direction, power, duration);
    }
}
