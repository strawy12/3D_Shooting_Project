using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public EItemType ItemType { get; set; }
    public void TakeAction();
}
