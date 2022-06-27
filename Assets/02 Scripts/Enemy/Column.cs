using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Column : PoolableMono
{
    static private readonly float height = 3.75f;
    [SerializeField] private Transform _mainMeshTrm;
    private MeshRenderer _meshRenderer;

    [HideInInspector]
    public UnityEvent<Collider> OnColliderEnter;


    public void Aspire(float spawnTime, float disableTime, float effectOffset, float lifeTime)
    {
        Sequence seq = DOTween.Sequence();

        if(_meshRenderer == null)
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
        transform.position -= Vector3.up * height;

        Color color = _meshRenderer.material.color;
        color.a = 1f;
        _meshRenderer.material.color = color;


        GenerateEffect(spawnTime + effectOffset);
        seq.Append(transform.DOMoveY(transform.position.y + height, spawnTime).SetEase(Ease.InCubic));
        seq.Join(_mainMeshTrm.DOShakePosition(spawnTime + effectOffset, 0.5f, 10, 90));
        seq.AppendCallback(() => DisapearColumn(disableTime, lifeTime));
    }
    private void DisapearColumn(float disableTime, float lifeTime)
    {
        _meshRenderer.material.DOFade(0f, disableTime).SetDelay(lifeTime).OnComplete(ColumnDisable);
    }

    private void ColumnDisable()
    {


        PoolManager.Inst.Push(this);
    }

    private void GenerateEffect(float lifeTime)
    {
        DustEffect effect = PoolManager.Inst.Pop("DustEffect")as DustEffect;

        if(effect != null)
        {
            Vector3 pos = transform.position;
            pos.y = 0f;
            effect.SetPositionAndRotation(pos, transform.rotation);
            effect.StartEffect(lifeTime + 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnColliderEnter?.Invoke(other);
    }

    public override void Reset()
    {
        OnColliderEnter.RemoveAllListeners();
    }
}
