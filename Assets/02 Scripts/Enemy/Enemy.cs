using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : MonoBehaviour, IHittable
{
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    //�ӽ� �ڵ� ���ʹ� ������ ��������
    [SerializeField] private int damage;
    [SerializeField] private float _hitEffectLifeTime;
    [SerializeField] private Transform _hitEffectPos;
    [SerializeField] private Material _hitMat;

    private SkinnedMeshRenderer _skinnedMeshRederer;

    private bool _isDead = false;


    public bool IsEnemy { get; set; }
    public int Health { get; private set; }
    public Vector3 HitPoint { get; set; }
    public int HitCount { get; set; }

    public UnityEvent OnDie;
    public UnityEvent OnGetHit;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
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
        _enemyAttack.Attack(damage);
    }
}
