using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiTanItem : ItemObject, IItem
{
    [field:SerializeField]
    public EItemType ItemType { get; set; }

    public void TakeAction()
    {
        GameManager.Inst.Data.AddCount(ItemType);
    }
}
