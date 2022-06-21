using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingData;
    [SerializeField] private List<SpawnPair> _spawnPairList;

    private SpawnPair _currentSpawnPair;

    private void Awake()
    {
        new PoolManager(transform);

        foreach (var pair in _poolingData.list)
        {
            PoolManager.Inst.CreatePool(pair.prefab, pair.poolCnt);
        }

        // EventManager Enemy 죽을때마다 실행시켜서 여기서 작동
        EventManager.StartListening(Constant.ALL_KILL_MONSTER, () => NextSpawnInfo(ChangeSpawnPairType.AllKill));

    }

    private void Start()
    {
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        if (_spawnPairList.Count == 0) return;

        _currentSpawnPair = _spawnPairList[0];
        _spawnPairList.RemoveAt(0);

        StartCoroutine(SpawnMonsterCoroutine());
    }

    private void NextSpawnInfo(ChangeSpawnPairType type)
    {
        if (_currentSpawnPair.isSpawning) return;
        if (_currentSpawnPair.changeType != type) return;

        if (_currentSpawnPair.monsterSpawnInfoDataSO.Count == 0)
        {
            NextElement();
            return;
        }

        SpawnMonster();
    }

    private IEnumerator SpawnMonsterCoroutine()
    {
        Enemy enemy = null;
        _currentSpawnPair.isSpawning = true;
        int cnt = _currentSpawnPair.monsterSpawnInfoDataSO.Count;
        MonsterSpawnInfo info = null;
        for (int i = 0; i < cnt; i++)
        {
            info = _currentSpawnPair.monsterSpawnInfoDataSO[i];
            for (int j = 0; j < info.spawnCnt; j++)
            {
                enemy = PoolManager.Inst.Pop(info.enemy.name) as Enemy;
                enemy.SetMonsterData(info.monsterData);
                enemy.transform.position = _currentSpawnPair.spawnPos.position;
                enemy.transform.rotation = _currentSpawnPair.spawnPos.rotation;
                Debug.Log(enemy.transform.position);
                Debug.Log(_currentSpawnPair.spawnPos.position);

                yield return new WaitForSeconds(info.nextSpawnDelay);
            }
            Debug.Log(12);

            yield return new WaitForSeconds(_currentSpawnPair.monsterSpawnInfoDataSO.nextElementSpawnDelay);
        }

        _currentSpawnPair.isSpawning = false;

        if (_currentSpawnPair.changeType == ChangeSpawnPairType.Delay)
        {
            SpawnMonster();
        }
    }

    private void NextElement()
    {

    }

    private void OnApplicationQuit()
    {
        EventManager.StartListening(Constant.ALL_KILL_MONSTER, () => NextSpawnInfo(ChangeSpawnPairType.AllKill));
    }

    private void OnDestroy()
    {
        EventManager.StartListening(Constant.ALL_KILL_MONSTER, () => NextSpawnInfo(ChangeSpawnPairType.AllKill));
    }
}
