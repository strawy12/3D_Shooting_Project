using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public int attackDamage;
    public int health;
    [HideInInspector]
    public bool isLastMonster;
    public MonsterData(MonsterData data)
    {
        health = data.health;
        attackDamage = data.attackDamage;
        isLastMonster = data.isLastMonster;
    }
}
[System.Serializable]
public class MonsterSpawnInfo
{
    public Enemy enemy;
    public MonsterData monsterData;
    public int spawnCnt;
    public float nextSpawnDelay;

}

[CreateAssetMenu(menuName = "SO/System/MonsterSpawnInfoDataSO")]
public class MonsterSpawnInfoDataSO : ScriptableObject
{
    public List<MonsterSpawnInfo> monsterSpawnInfoDataList;
    
    public MonsterSpawnInfo this[int idx]
    {
        get => monsterSpawnInfoDataList[idx];
        set => monsterSpawnInfoDataList[idx] = value;
    }

    public int Count { get => monsterSpawnInfoDataList.Count; }

    public float nextElementSpawnDelay;
}
