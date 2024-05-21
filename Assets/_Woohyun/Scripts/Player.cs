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

    //������ ���� ǥ��
    [SerializeField]
    private GameObject ItemInfo;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemValueText;

    private Item nearbyItem;
    private Camera mainCamera;
    private int currentSlot = -1;

    void Start()
    {
        mainCamera = Camera.main;
        ItemInfo.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemValueText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckForItem();

        if (nearbyItem != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickupItem();
            }
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


    void CheckForItem() //����ĳ��Ʈ ������ Ž�� Ȯ��
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
                itemNameText.text = item.itemName;
                itemValueText.text = item.value.ToString() + "��";
                ItemInfo.SetActive(true);
                itemNameText.gameObject.SetActive(true);
                itemValueText.gameObject.SetActive(true);
                return;
            }
        }

        nearbyItem = null;
        ItemInfo.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemValueText.gameObject.SetActive(false);
    }

    void PickupItem()
    {
        if (nearbyItem != null)
        {
            quickSlot.AddItemToSlot(nearbyItem);
            nearbyItem.Pickup();
            nearbyItem = null;
            itemNameText.gameObject.SetActive(false);
        }
    }

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
}
