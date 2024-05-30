using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SelectQuickSlot;
    [SerializeField]
    private float pickupRange = 2.0f;
    [SerializeField]
    private LayerMask itemLayer;

    private Item nearbyItem;
    private DayPassTrigger nearbyDayAdvanceTrigger;
    private Camera mainCamera;
    private int currentSlot = 0;



    private static Player instance;



    private void Awake()
    {
        // Singleton ������ ����Ͽ� �ߺ� �ν��Ͻ��� ������ �ʵ��� �մϴ�.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        mainCamera = Camera.main;
        UIManager.Instance.ShowItemInfo(false);
        InventoryManager.Instance.RestoreItems();
    }

    void Update()
    {
        Debug.Log(MoveMap.isPlayerInTrigger);
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

        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red); // ����׿� ����ĳ��Ʈ Ȯ��

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
        if (MoveMap.isPlayerInTrigger == false)
        {
            UIManager.Instance.ShowInteractionMessage("", false);
        }
    }

    void PickupItem()
    {
        if (nearbyItem != null)
        {
            if (UIManager.Instance.IsQuickSlotFull())
            {
                Debug.Log("�������� ��� �� á���ϴ�!");
                return;
            }

            int slotIndex = UIManager.Instance.AddItemToQuickSlot(nearbyItem);
            if (slotIndex != -1)
            {
                InventoryManager.Instance.AddItem(nearbyItem); // �κ��丮�� ������ �߰�
                nearbyItem.Pickup(); // ������ ������Ʈ ��Ȱ��ȭ
                nearbyItem = null;
                UIManager.Instance.ShowItemInfo(false);
            }
        }
    }

    void DropItem()
    {
        Item itemToDrop = UIManager.Instance.GetSelectedQuickSlotItem(currentSlot);
        if (itemToDrop != null)
        {
            Vector3 dropPosition = transform.position + transform.forward + transform.up;
            itemToDrop.Drop(dropPosition);
            InventoryManager.Instance.RemoveItem(itemToDrop); // �κ��丮���� ������ ���� �� ������ ��ȯ
            UIManager.Instance.RemoveItemFromQuickSlot(currentSlot);
            Debug.Log($"{currentSlot + 1}�� ���� ������ ���");
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
