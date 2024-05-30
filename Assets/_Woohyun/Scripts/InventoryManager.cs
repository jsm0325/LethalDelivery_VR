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
            DontDestroyOnLoad(item.gameObject); // �ش� �����۸� DontDestroyOnLoad ó��
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            // �� ��ȯ �� �������� Ȱ��ȭ ���¸� �����ϵ��� �߰� ó���� �� �� �ֽ��ϴ�.
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
