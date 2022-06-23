using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    HoshiTan,
    Trap,
    Sheild
}


[System.Serializable]
public class PlayerData
{
    public Dictionary<EItemType, int> itemCountDict;

    public int GetItemCount(EItemType type)
    {
        int cnt;
        itemCountDict.TryGetValue(type, out cnt);
        return cnt;
    }

    public void InitItemDict()
    {
        itemCountDict = new Dictionary<EItemType, int>();
        itemCountDict.Add(EItemType.HoshiTan, 0);
        itemCountDict.Add(EItemType.Sheild, 0);
        itemCountDict.Add(EItemType.Trap, 0);
    }

    public void AddCount(EItemType type)
    {
        itemCountDict[type]++;
    }

    public void SubCount(EItemType type)
    {
        itemCountDict[type]--;
    }


}
