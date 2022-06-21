using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : PoolableMono, IHittable
{
    private EnemyAttack[] _enemyAttacks;
    private EnemyMovement _enemyMovement;
    //임시 코드 에너미 데이터 만들어야함
    
    [SerializeField] private float _hitEffectLifeTime;
    [SerializeField] private Transform _hitEffectPos;
    [SerializeField] private Material _hitMat;

    private SkinnedMeshRenderer _skinnedMeshRederer;

    private bool _isDead = false;
    private MonsterData _monsterData;

    public bool IsEnemy { get; set; }
    public int Health { get; private set; }
    public Vector3 HitPoint { get; set; }
    public int HitCount { get; set; }

    public UnityEvent OnDie;
    public UnityEvent OnGetHit;
    public UnityEvent OnSpawn;

    private bool _init;
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Health = 1000;

        if (_init) return;

        _init = true;
        _enemyAttacks = GetComponents<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _skinnedMeshRederer = GetComponentInChildren<SkinnedMeshRenderer>();

        Material[] temp = new Material[2];
        temp[0] = _skinnedMeshRederer.material;
        temp[1] = _hitMat;

        _skinnedMeshRederer.materials = temp;

        _hitMat = _skinnedMeshRederer.materials[1];
        _hitMat.SetFloat("_FresnelPower", 0f);
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

        OnGetHit?.Invoke();
        ShowHitOutline();
        GenerateHitEffect();

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

    public void SetMonsterData(MonsterData data)
    {
        _monsterData = new MonsterData(data);
    }

    public void SpawnEnemy(Vector3 pos)
    {
        transform.position = pos;
        OnSpawn?.Invoke();
    }

    private void ShowHitOutline()
    {
        DOTween.Kill(this);

        _hitMat.SetFloat("_FresnelPower", 10f);

        DOTween.To(() => _hitMat.GetFloat("_FresnelPower"),
                value => _hitMat.SetFloat("_FresnelPower", value),
                0f,
                _hitEffectLifeTime);
    }


    public void GenerateHitEffect()
    {
        ImpactEffect hitEffect = PoolManager.Inst.Pop($"HitEffect_{HitCount}") as ImpactEffect;
        hitEffect.SetPositionAndRotation(_hitEffectPos.position, Quaternion.LookRotation(transform.forward));
        hitEffect.StartEffect();
    }

    public void PerformAttack()
    {
        foreach (var attack in _enemyAttacks)
        {
            if (attack.AttackType == EnemyAttack.EAttackType.Default)
            {
               // attack.Attack(_monsterData.attackDamage);
                return;
            }
        }
    }

    public void EnemyDead()
    {
        if(_monsterData.isLastMonster)
        {
            EventManager.TriggerEvent(Constant.ALL_KILL_MONSTER);
        }
    }

    public override void Reset()
    {

    }
}
