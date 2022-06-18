using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.2f;
    [SerializeField] private float _generateInterval = 0.2f;
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartEffect()
    {
        StartCoroutine(EffectLoop());
    }

    private IEnumerator EffectLoop()
    {
        while (true)
        {
            AfterImageEffect temp = PoolManager.Inst.Pop("AfterImageEffect") as AfterImageEffect;
            temp.gameObject.SetActive(true);

            temp.SetModelAnim(_animator, _lifeTime);
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(_generateInterval);
        }
    }

    public void StopEffect()
    {
        StopAllCoroutines();
    }
}
