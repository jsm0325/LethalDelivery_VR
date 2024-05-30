using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class ItemData
    {
        public string itemID;
        public string itemName;
        public Vector3 position;
        public bool isPickedUp;
    }

    private List<ItemData> itemDataList = new List<ItemData>();
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "itemData.json");
            LoadItemData();
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 호출
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RestoreItems();
    }

    public void AddItem(Item item)
    {
        DontDestroyOnLoad(item.gameObject);
        ItemData itemData = itemDataList.Find(data => data.itemID == item.itemID);
        if (itemData == null)
        {
            itemDataList.Add(new ItemData
            {
                itemID = item.itemID,
                itemName = item.itemName,
                position = item.transform.position,
                isPickedUp = true
            });
        }
        else
        {
            itemData.isPickedUp = true;
            itemData.position = item.transform.position;
        }
        SaveItemData();
    }

    public void RemoveItem(Item item)
    {
        ItemData itemData = itemDataList.Find(data => data.itemID == item.itemID);
        if (itemData != null)
        {
            itemData.isPickedUp = false;
            itemData.position = item.transform.position;
            item.ReturnToScene(); // 아이템을 현재 씬으로 이동
            SaveItemData();
        }
    }

    public void ClearItemData()
    {
        itemDataList.Clear();
        SaveItemData();
    }

    public void SaveItemData()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<ItemData>(itemDataList), true);
        File.WriteAllText(saveFilePath, jsonData);
    }

    private void LoadItemData()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            itemDataList = JsonUtility.FromJson<Serialization<ItemData>>(jsonData).ToList();
        }
    }

    public void RestoreItems()
    {
        foreach (var itemData in itemDataList)
        {
            if (!itemData.isPickedUp)
            {
                GameObject itemPrefab = Resources.Load<GameObject>($"Prefabs/{itemData.itemName}");
                if (itemPrefab != null)
                {
                    GameObject itemObject = Instantiate(itemPrefab, itemData.position, Quaternion.identity);
                    Item item = itemObject.GetComponent<Item>();
                    if (item != null)
                    {
                        item.SetItemID(itemData.itemID);
                    }
                }
            }
        }
    }
}

[Serializable]
public class Serialization<T>
{
    public List<T> target;

    public Serialization(List<T> target)
    {
        this.target = target;
    }

    public List<T> ToList()
    {
        return target;
    }
}
