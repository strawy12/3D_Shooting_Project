using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildItem : ItemObject, IItem
{
    [field: SerializeField]
    public EItemType ItemType { get; set; }

    public void TakeAction()
    {
        Define.PlayerRef.GetComponent<Player>();
        OnTriggerEnter.Invoke();
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        ItemType = EItemType.Sheild;
    }
}
