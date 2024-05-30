using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<Item.ItemData> pickedUpItems = new List<Item.ItemData>();
    private Item.ItemData[] quickSlotItems = new Item.ItemData[4];

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

    public void AddItem(Item.ItemData item)
    {
        pickedUpItems.Add(item);
    }

    public void RemoveItem(Item.ItemData item)
    {
        pickedUpItems.Remove(item);
    }

    public List<Item.ItemData> GetPickedUpItems()
    {
        return new List<Item.ItemData>(pickedUpItems);
    }

    public void SetQuickSlotItem(int slotIndex, Item.ItemData itemData)
    {
        if (slotIndex >= 0 && slotIndex < quickSlotItems.Length)
        {
            quickSlotItems[slotIndex] = itemData;
        }
    }

    public Item.ItemData GetQuickSlotItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < quickSlotItems.Length)
        {
            return quickSlotItems[slotIndex];
        }
        return null;
    }

    public void ClearQuickSlotItems()
    {
        for (int i = 0; i < quickSlotItems.Length; i++)
        {
            quickSlotItems[i] = null;
        }
    }
}
