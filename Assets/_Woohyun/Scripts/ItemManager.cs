using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> itemPrefabs; // 아이템 프리팹 리스트
    [SerializeField]
    private List<Transform> spawnLocations; // 아이템 스폰 위치 리스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnItems();
    }

    // 아이템들을 랜덤 위치에 스폰
    private void SpawnItems()
    {
        foreach (var location in spawnLocations)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[randomIndex], location.position, location.rotation);
        }
    }

    // 특정 위치에 아이템을 드롭
    public void DropItem(Item item, Vector3 dropPosition)
    {
        GameObject itemPrefab = itemPrefabs.Find(prefab => prefab.GetComponent<Item>().itemID == item.itemID);
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }
}
