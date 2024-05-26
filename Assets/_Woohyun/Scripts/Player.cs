using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private QuickSlot quickSlot;
    [SerializeField]
    private GameObject[] SelectQuickSlot;
    [SerializeField]
    private float pickupRange = 2.0f;
    [SerializeField]
    private LayerMask itemLayer;

    private Item nearbyItem;
    private DayPassTrigger nearbyDayAdvanceTrigger;
    private Camera mainCamera;
    private int currentSlot = -1;

    void Start()
    {
        mainCamera = Camera.main;
        UIManager.Instance.ShowItemInfo(false);
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        
        CheckForItem();
        CheckForDayAdvanceTrigger();

        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }

        if (nearbyDayAdvanceTrigger != null && Input.GetKeyDown(KeyCode.F))
        {
            DayPass();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
        }
    }

    void SelectSlot(int index)
    {
        if (currentSlot != -1)
        {
            SelectQuickSlot[currentSlot].SetActive(false);
        }
        currentSlot = index;
        SelectQuickSlot[currentSlot].SetActive(true);
    }

    //����ĳ��Ʈ�� ������ Ž�� Ȯ��
    void CheckForItem() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); //����׿� ����ĳ��Ʈ Ȯ��

        if (Physics.Raycast(ray, out hit, pickupRange, itemLayer))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                nearbyItem = item;
                UIManager.Instance.UpdateItemInfoUI(item.itemName, item.value);
                UIManager.Instance.ShowItemInfo(true);
                return;
            }
        }

        nearbyItem = null;
        UIManager.Instance.ShowItemInfo(false);
    }

    //����ĳ��Ʈ�� ��¥ �ѱ�� Ʈ���� üũ
    void CheckForDayAdvanceTrigger()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            DayPassTrigger dayAdvanceTrigger = hit.collider.GetComponent<DayPassTrigger>();
            if (dayAdvanceTrigger != null)
            {
                nearbyDayAdvanceTrigger = dayAdvanceTrigger;
                UIManager.Instance.ShowInteractionMessage("F ��¥ �ѱ�", true);
                return;
            }
        }

        nearbyDayAdvanceTrigger = null;
        UIManager.Instance.ShowInteractionMessage("", false);
    }

    //������ �ݱ�
    void PickupItem()
    {
        if (nearbyItem != null)
        {
            quickSlot.AddItemToSlot(nearbyItem);
            nearbyItem.Pickup();
            nearbyItem = null;
            UIManager.Instance.ShowItemInfo(false);
        }
    }

    //������ ������
    void DropItem()
    {
        Item itemToDrop = quickSlot.GetSelectedSlotItem(currentSlot);
        if (itemToDrop != null)
        {
            Vector3 dropPosition = transform.position + transform.forward;
            itemToDrop.gameObject.SetActive(true);
            itemToDrop.transform.position = dropPosition; // ���� ��ġ�� ������ ���
            quickSlot.RemoveItemFromSlot(currentSlot);
            Debug.Log(currentSlot + 1 + "�� ���� ������ ���");
        }
    }

    // �Ϸ� �ѱ�� ��ư���� FŰ ������ �� ó��
    void DayPass()
    {
        GameManager.Instance.DayPass();
        nearbyDayAdvanceTrigger = null;
        UIManager.Instance.ShowInteractionMessage("", false);
    }
}
