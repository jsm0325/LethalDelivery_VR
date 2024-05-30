using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
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
            SceneManager.sceneLoaded += OnSceneLoaded;

            InitializeAllItemData(); // 모든 씬의 데이터 초기화
            UpdateSaveFilePath(); // 현재 씬 파일 경로 설정
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
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateSaveFilePath();
        LoadItemData();
        RestoreItems();
    }

    private void UpdateSaveFilePath()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        saveFilePath = Path.Combine(Application.persistentDataPath, $"{sceneName}_itemData.json");
    }

    private void InitializeAllItemData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*_itemData.json");
        foreach (var file in files)
        {
            File.Delete(file);
        }
        itemDataList.Clear();
    }

    public void AddItem(Item item)
    {
        DontDestroyOnLoad(item.gameObject);
        ItemData itemData = itemDataList.Find(data => data.itemID == item.itemID);
        if (itemData != null)
        {
            itemDataList.Remove(itemData);
        }
    }

    public void RemoveItem(Item item)
    {
        ItemData itemData = itemDataList.Find(data => data.itemID == item.itemID);
        if (itemData == null)
        {
            itemData = new ItemData
            {
                itemID = item.itemID,
                itemName = item.itemName,
                position = item.transform.position,
                isPickedUp = false
            };
            itemDataList.Add(itemData);
        }
        else
        {
            itemData.isPickedUp = false;
            itemData.position = item.transform.position;
        }
        item.ReturnToScene();
        SaveItemData();
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
        else
        {
            itemDataList.Clear();
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

    public void RemoveItemData(string itemID)
    {
        ItemData itemData = itemDataList.Find(data => data.itemID == itemID);
        if (itemData != null)
        {
            itemDataList.Remove(itemData);
            SaveItemData();
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
