using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<Item> items = new List<Item>();

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

    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            DontDestroyOnLoad(item.gameObject); // 해당 아이템만 DontDestroyOnLoad 처리
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            // 씬 전환 시 아이템이 활성화 상태를 유지하도록 추가 처리를 할 수 있습니다.
        }
    }

    public Item GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public int GetItemCount()
    {
        return items.Count;
    }
}
