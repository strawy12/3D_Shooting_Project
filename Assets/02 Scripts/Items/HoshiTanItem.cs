using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanItem : ItemObject, IItem
{
    [field:SerializeField]
    public EItemType ItemType { get; set; }

    public void TakeAction()
    {
        GameManager.Inst.AddItemCount(ItemType);
        OnTriggerEnter.Invoke();
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        ItemType = EItemType.HoshiTan;
    }
}
