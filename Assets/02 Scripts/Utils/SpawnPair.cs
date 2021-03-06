using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public bool isSpawning;
    public Transform spawnPos;
    public MonsterSpawnInfoDataSO monsterSpawnInfoDataSO;
    public ChangeSpawnPairType changeType;
    public float spawnDelay;
    public int actionNum = -1;
    public UnityEvent endSpawnAction;
}