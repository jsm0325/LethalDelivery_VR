using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField]
    private Image[] slotImages;
    private Item[] items;

    private void Start()
    {
        items = new Item[slotImages.Length];
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].enabled = false;
        }
    }

    // 퀵슬롯이 모두 꽉 찼는지 확인하는 메서드
    public bool IsFull()
    {
        foreach (Image slotImage in slotImages)
        {
            if (!slotImage.enabled)
            {
                return false; // 빈 슬롯이 하나라도 있으면 꽉 차지 않은 상태
            }
        }
        return true; // 모든 슬롯이 채워져 있으면 꽉 찬 상태
    }

    public void AddItemToSlot(Item item)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (!slotImages[i].enabled)
            {
                slotImages[i].sprite = item.icon;
                slotImages[i].enabled = true;
                items[i] = item;
                Debug.Log($"{item.itemName} 아이템 {i + 1}번 슬롯에 추가");
                break;
            }
        }
    }

    public void RemoveItemFromSlot(int index)
    {
        if (index >= 0 && index < slotImages.Length)
        {
            slotImages[index].sprite = null;
            slotImages[index].enabled = false;
            items[index] = null;
        }
    }

    public Item GetSelectedSlotItem(int index)
    {
        if (index >= 0 && index < slotImages.Length && slotImages[index].enabled)
        {
            return items[index];
        }
        return null;
    }
}