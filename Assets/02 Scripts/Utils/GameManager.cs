using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChangeSpawnPairType
{
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
}

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingData;
    [SerializeField] private List<SpawnPair> _spawnPairList;

    private void Awake()
    {
        new PoolManager(transform);

        foreach (var pair in _poolingData.list)
        {
            PoolManager.Inst.CreatePool(pair.prefab, pair.poolCnt);
        }

        // EventManager Enemy 죽을때마다 실행시켜서 여기서 작동
    }

    private void SpawnMonster()
    {

    }

    private void NextSpawnInfo()
    {

    }

    private void NextElement()
    {

    }
}
