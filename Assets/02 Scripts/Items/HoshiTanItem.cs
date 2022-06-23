using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanItem : ItemObject, IItem
{
    [field:SerializeField]
    public EItemType ItemType { get; set; }

    [SerializeField] private float _rotateSpped;

    protected override void Update()
    {
        base.Update();
        Vector3 angle = transform.eulerAngles;
        angle.y += Time.deltaTime * _rotateSpped;

        transform.rotation = Quaternion.Euler(angle); 
    }

    public void TakeAction()
    {
        GameManager.Inst.AddItemCount(ItemType);
        OnTriggerEnter.Invoke();
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        
    }
}
