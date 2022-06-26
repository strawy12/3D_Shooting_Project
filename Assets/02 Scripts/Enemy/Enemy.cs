using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : PoolableMono, IHittable
{
    private EnemyAttack[] _enemyAttacks;
    private EnemyMovement _enemyMovement;
    //임시 코드 에너미 데이터 만들어야함

    [SerializeField] private float _hitEffectLifeTime;
    [SerializeField] private Transform _hitEffectPos;
    [SerializeField] private GameObject _hitCollider;
    [SerializeField] private Material _hitMat;

    private SkinnedMeshRenderer _skinnedMeshRederer;
    private NavMeshAgent _navMeshAgent;

    private bool _isDead = false;

    private MonsterData _monsterData;

    public bool IsEnemy { get; set; }
    public Vector3 HitPoint { get; set; }
    public int HitCount { get; set; }

    public bool CanAttack { get; set; }

    public UnityEvent OnDie;
    public UnityEvent OnGetHit;
    public UnityEvent OnSpawn;

    private float _additionalDamageFactor = 1f;
    private Coroutine _hoshiTanEffectCoroutine = null;

    BuffEffect _currentBuffEffect;

    private bool _init;
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (!_init)
        {
            _init = true;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.enabled = false;
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
    }

    public void GetHit(int damage, GameObject damagerDealer)
    {
        float critical = Random.value;
        bool isCritical = false;

        if (_isDead) return;

        //if (critical <= GameManager.Inst.criticalChance)
        //{
        //    float ratio = Random.Range(GameManager.Inst.criticalMinDamage, GameManager.Inst.criticalMaxDamage);

        //    damage = Mathf.CeilToInt((float)damage * ratio);

        //    isCritical = true;
        //}

        _monsterData.health -=  (int)(damage * _additionalDamageFactor);

        ShowHitOutline();
        GenerateHitEffect((int)(damage * _additionalDamageFactor));
        //DamagePopup popup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        //popup.Setup(damage, transform.position + new Vector3(0, 0.5f, 0), isCritical);

        if (_monsterData.health <= 0f)
        {
            EnemyDead();
            OnDie?.Invoke();
        }

        else
        {
            OnGetHit?.Invoke();
        }


    }

    public void SetMonsterData(MonsterData data, bool isLast)
    {
        _monsterData = new MonsterData(data);
        _monsterData.isLastMonster = isLast;
    }

    public void SpawnEnemy()
    {
        OnSpawn?.Invoke();
        _navMeshAgent.enabled = true;
        _hitCollider.SetActive(true);
        _enemyMovement.StartMove();
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


    public void GenerateHitEffect(int damage)
    {
        GameManager.Inst.UI.GenerateDamagePopup(_hitEffectPos.position, damage, false);
    }

    public void PerformAttack()
    {
        if (_isDead) return;

        foreach (var attack in _enemyAttacks)
        {
            if (attack.AttackType == EnemyAttack.EAttackType.Default)
            {
                attack.Attack(_monsterData.attackDamage);
                return;
            }
        }
    }

    public void EnemyDead()
    {
        StopAllCoroutines();
        _hitCollider.SetActive(false);
        _isDead = true;
        CanAttack = false;
        _enemyMovement.StopImmediatelly();

        GameManager.Inst.UI.GenerateKillText("Snake");
        if (_monsterData.isLastMonster)
        {
            EventManager.TriggerEvent(Constant.ALL_KILL_MONSTER);
        }


        if (_additionalDamageFactor != 1f)
        {
            _additionalDamageFactor = 1f;
            _currentBuffEffect.ImmediatelyStop();
        }

        StartCoroutine(DeadDelay());
    }

    private IEnumerator DeadDelay()
    {
        yield return new WaitForSeconds(3f);

        PoolManager.Inst.Push(this);
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public override void Reset()
    {
        CanAttack = true;
        _isDead = false;
        _skinnedMeshRederer.material.DOFade(1f, 0f);

    }

    public void GetHoshiTanEffect(float duration)
    {
        _additionalDamageFactor = 1.5f;

        BuffEffect effect = PoolManager.Inst.Pop("DebuffEffect") as BuffEffect;
        _currentBuffEffect?.ImmediatelyStop();
        effect.transform.SetParent(_hitEffectPos);
        effect.transform.localPosition = Vector3.zero;
        effect.gameObject.SetActive(true);
        _currentBuffEffect = effect;

        effect.StartEffect(duration);

        if (_hoshiTanEffectCoroutine != null)
        {
            StopCoroutine(_hoshiTanEffectCoroutine);
            _hoshiTanEffectCoroutine = null;
        }

        _hoshiTanEffectCoroutine = StartCoroutine(HoshiTanEffectCoroutine(duration));
    }

    private IEnumerator HoshiTanEffectCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        _additionalDamageFactor = 1f;
    }

    public void GetHitTrapEffect(float duration)
    {
        _enemyMovement.StopImmediatelly();
        StartCoroutine(TrapEffectDelay(duration));
    }
    private IEnumerator TrapEffectDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        _enemyMovement.StartMove();
    }
}
