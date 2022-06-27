using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private EItemType _spawnItemType;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector3 _spawnPosOffset;

    private void Start()
    {
        SpawnItem();
    }

    public IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(_spawnDelay);

        SpawnItem();
    }
    

    private void SpawnItem()
    {
        ItemObject item = PoolManager.Inst.Pop($"{_spawnItemType}_Item") as ItemObject;

        item.transform.position = transform.position + _spawnPosOffset;
        item.OnTriggerEnter.RemoveAllListeners();
        item.OnTriggerEnter.AddListener(() => StartCoroutine(SpawnDelay()));

        if (!item.gameObject.activeSelf)
        {
            item.gameObject.SetActive(true);
        }

    }
}
