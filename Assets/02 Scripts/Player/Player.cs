using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHittable
{
    [SerializeField]
    private int _maxHealth;

    private int _health;
    private int _shielHealth;

    [SerializeField] private float _recoverySpeed;

    private bool _isDead = false;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }
    public UnityEvent OnGetShieldHit;
    public UnityEvent OnGetItem;
    public UnityEvent OnShieldBreak;

    public Vector3 HitPoint { get; set; }

    //피아 식별용    
    public bool IsEnemy => false;

    public int HitCount { get; set; }

    public bool CanAttack { get; set; }

    [SerializeField] private Transform _hitEffectPos;

    private BuffEffect _currentBuffEffect;

    //넉백 처리를 위한 에이전트 무브먼트 가져오기
    private PlayerMovement _playerMovement;
    private ShieldEffect _currentSheild;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _health = _maxHealth;
        GameManager.Inst.UI.SetHpBar(_health, _maxHealth, _shielHealth);
    }
    private void OnTriggerEnter(Collider other) // 아이템용
    {
        if (other.gameObject.CompareTag("Item"))
        {
            IItem item = other.transform.GetComponentInParent<IItem>();
            OnGetItem?.Invoke();
            if (item.ItemType == EItemType.Sheild)
            {
                GenerateShield();
            }
            item.TakeAction();

        }
    }

    private void GenerateShield()
    {
        if(_currentSheild == null)
        {
            _currentSheild = PoolManager.Inst.Pop("ShieldEffect") as ShieldEffect;
            _currentSheild.transform.SetParent(transform);
            _currentSheild.transform.localPosition = Vector3.up * 0.75f;
            _currentSheild.transform.localRotation = Quaternion.identity;
            _currentSheild.StartEffect();
        }

        _shielHealth += (int)(_maxHealth / 8);

        GameManager.Inst.UI.SetHpBar(_health, _maxHealth, _shielHealth);
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        if(_shielHealth > 0f)
        {
            _shielHealth -= damage;
            damage -= _shielHealth;
            OnGetShieldHit?.Invoke();
        }

        if (damage > 0f)
        {
            _health -= damage;
            OnGetHit?.Invoke();
            GenerateHitEffect();
        }

        if (_currentSheild != null && _shielHealth <= 0f )
        {
            OnShieldBreak?.Invoke();

            _currentSheild.EndEffect();
            _currentSheild = null;
        }

        GameManager.Inst.UI.SetHpBar(_health, _maxHealth, _shielHealth);

        if (_health <= 0)
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

    public void GetHoshiTanEffect(float duration)
    {
        BuffEffect effect = PoolManager.Inst.Pop("HealEffect") as BuffEffect;
        _currentBuffEffect?.ImmediatelyStop();
        effect.transform.SetParent(_hitEffectPos);
        effect.transform.localPosition = Vector3.zero;
        effect.gameObject.SetActive(true);
        _currentBuffEffect = effect;

        effect.StartEffect(duration);

        StopAllCoroutines();
        StartCoroutine(HoshiTanEffectCoroutine(duration));
    }

    private IEnumerator HoshiTanEffectCoroutine(float duration)
    {
        float recoveryValue = 0f;
        while (duration > 0f)
        {
            recoveryValue += Time.deltaTime * _recoverySpeed;

            if (recoveryValue > 1)
            {
                recoveryValue = 0f;
                _health++;

                _health = Mathf.Clamp(_health, 0, _maxHealth);
                GameManager.Inst.UI.SetHpBar(_health, _maxHealth, _shielHealth);
            }

            duration -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
