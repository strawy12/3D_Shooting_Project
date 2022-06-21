using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    [Header("PoolingList")]
    public List<PoolingPair> list;

}
