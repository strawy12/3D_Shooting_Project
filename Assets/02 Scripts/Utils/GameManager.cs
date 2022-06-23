using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingData;
    [SerializeField] private List<SpawnPair> _spawnPairList;



    private SpawnPair _currentSpawnPair;
    private PlayerData _playerData;

    private UIManager _uiManager;

    public bool Test;

    public PlayerData Data
    {
        get
        {
            return _playerData;
        }
    }

    public UIManager UI
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = GetComponent<UIManager>();
            }

            return _uiManager;
        }
    }


    private void Awake()
    {
        new PoolManager(transform);

        foreach (var pair in _poolingData.list)
        {
            PoolManager.Inst.CreatePool(pair.prefab, pair.poolCnt);
        }

        // EventManager Enemy 죽을때마다 실행시켜서 여기서 작동

        if (_uiManager == null)
        {
            _uiManager = GetComponent<UIManager>();
        }
    }

    private void Start()
    {
        EventManager.StartListening(Constant.ALL_KILL_MONSTER, () => NextSpawnInfo(ChangeSpawnPairType.AllKill));

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

    int loopCnt = 0;
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
                enemy.SetPositionAndRotation(_currentSpawnPair.spawnPos.position, _currentSpawnPair.spawnPos.rotation);
                enemy.SpawnEnemy();

                if(Test)
                {
                    yield break;
                }
                yield return new WaitForSeconds(info.nextSpawnDelay);
            }
            yield return new WaitForSeconds(_currentSpawnPair.monsterSpawnInfoDataSO.nextElementSpawnDelay);

        }

        _currentSpawnPair.isSpawning = false;

        if (_currentSpawnPair.changeType == ChangeSpawnPairType.Delay)
        {
            yield return new WaitForSeconds(_currentSpawnPair.spawnDelay);

            SpawnMonster();
        }
    }

    public void AddCount(EItemType itemType)
    {
        SetItemPanel(itemType);
        _playerData.AddCount(itemType);
    }

    public void SubsCount(EItemType itemType)
    {
        SetItemPanel(itemType);
        _playerData.AddCount(itemType);
    }

    private void SetItemPanel(EItemType type)
    {
        ItemPanel panel = _uiManager.FindItemPanel(type);

        panel.SetCountText();
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
