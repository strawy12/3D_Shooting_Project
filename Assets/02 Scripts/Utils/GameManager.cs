using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Default,
    UI,
    CutScene
}

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingData;
    [SerializeField] private List<SpawnPair> _spawnPairList;

    private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }

    private SpawnPair _currentSpawnPair;
    private PlayerData _playerData;

    private UIManager _uiManager;

    public bool Test;

    private int _actionNum = 0;

    public PlayerData Data
    {
        get
        {
            if(_playerData == null)
            {
                _playerData = new PlayerData();
                _playerData.InitItemDict();
            }
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

        if (_playerData == null)
        {
            _playerData = new PlayerData();
            _playerData.InitItemDict();
        }
    }

    private void Start()
    {
        EventManager.StartListening(Constant.ALL_KILL_MONSTER, () => NextSpawnInfo(ChangeSpawnPairType.AllKill));
        EventManager.StartListening(Constant.ARRIVED_TARGET_POS, () => NextSpawnInfo(ChangeSpawnPairType.Arrived));
        PEventManager.StartListening(Constant.ON_TARGET_ACTION, NextSpawnInfo);

        SpawnMonster();
    }

    private void SpawnMonster()
    {
        if (_spawnPairList.Count == 0) return;

        _currentSpawnPair = _spawnPairList[0];
        _spawnPairList.RemoveAt(0);

        StartCoroutine(SpawnMonsterCoroutine());
    }
    private void NextSpawnInfo(Param param)
    {
        if (_actionNum != param.iParam) return;

        NextSpawnInfo(ChangeSpawnPairType.Action);
    }
    private void NextSpawnInfo(ChangeSpawnPairType type)
    {
        if (_currentSpawnPair.isSpawning) return;
        if (_currentSpawnPair.changeType != type) return;

        _currentSpawnPair.endSpawnAction?.Invoke();
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
                bool isLast = j == info.spawnCnt - 1;
                enemy.SetMonsterData(info.monsterData, isLast);
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

            NextSpawnInfo(ChangeSpawnPairType.Delay);
        }

        else if(_currentSpawnPair.changeType == ChangeSpawnPairType.Action)
        {
            if(_currentSpawnPair.actionNum == -1)
            {
                Debug.LogError("action 설정하고 넘버 설정 안함");
                yield break;
            }

            _actionNum = _currentSpawnPair.actionNum;
        }
    }

    public void AddItemCount(EItemType itemType)
    {
        _playerData.AddCount(itemType);
        SetItemPanel(itemType);
    }

    public void SubItemCount(EItemType itemType)
    {
        _playerData.SubCount(itemType);
        SetItemPanel(itemType);
    }

    private void SetItemPanel(EItemType type)
   {
        ItemPanel panel = _uiManager.FindItemPanel(type);
        panel.SetCountText();
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
