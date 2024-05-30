using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> itemPrefabs; // ������ ������ ����Ʈ
    [SerializeField]
    private List<Transform> spawnLocations; // ������ ���� ��ġ ����Ʈ

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

    // �����۵��� ���� ��ġ�� ����
    private void SpawnItems()
    {
        foreach (var location in spawnLocations)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[randomIndex], location.position, location.rotation);
        }
    }

    // Ư�� ��ġ�� �������� ���
    public void DropItem(Item item, Vector3 dropPosition)
    {
        GameObject itemPrefab = itemPrefabs.Find(prefab => prefab.GetComponent<Item>().itemID == item.itemID);
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }
}
