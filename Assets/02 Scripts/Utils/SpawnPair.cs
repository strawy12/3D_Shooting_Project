using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChangeSpawnPairType
{
    Delay,
    AllKill,
    Arrived,
    Action
}

[System.Serializable]
public class SpawnPair
{
    [HideInInspector]
    public bool isSpawning;
    public Transform spawnPos;
    public MonsterSpawnInfoDataSO monsterSpawnInfoDataSO;
    public ChangeSpawnPairType changeType;
    public float spawnDelay;
}