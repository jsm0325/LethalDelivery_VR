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

    public GameObject[] quickSlotPrefabs = new GameObject[4];

    private static Player instance;



    private void Awake()
    {
        // Singleton 패턴을 사용하여 중복 인스턴스가 생기지 않도록 합니다.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 삭제되지 않도록 설정
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

    //레이캐스트로 아이템 탐색 확인
    void CheckForItem()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red); // 디버그용 레이캐스트 확인

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

    //레이캐스트로 날짜 넘기는 트리거 체크
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
                UIManager.Instance.ShowInteractionMessage("F 날짜 넘김", true);
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
                Debug.Log("퀵슬롯이 모두 꽉 찼습니다!");
                return;
            }

            int slotIndex = UIManager.Instance.AddItemToQuickSlot(nearbyItem);
            if (slotIndex != -1)
            {
                quickSlotPrefabs[slotIndex] = nearbyItem.gameObject; // 해당 퀵슬롯에 프리팹 저장
                InventoryManager.Instance.AddItem(nearbyItem); // 인벤토리에 아이템 추가
                nearbyItem.Pickup(); // 아이템 오브젝트 비활성화
                nearbyItem = null;
                UIManager.Instance.ShowItemInfo(false);
            }
        }
    }

    void DropItem()
    {
        Item itemToDrop = UIManager.Instance.GetSelectedQuickSlotItem(currentSlot);
        if (itemToDrop != null && quickSlotPrefabs[currentSlot] != null)
        {
            Vector3 dropPosition = transform.position + transform.forward + transform.up;
            GameObject droppedItem = Instantiate(quickSlotPrefabs[currentSlot], dropPosition, Quaternion.identity); // 저장된 프리팹을 사용하여 아이템 생성
            itemToDrop.Drop(dropPosition); // 아이템 오브젝트 활성화 및 위치 재설정
            InventoryManager.Instance.RemoveItem(itemToDrop); // 인벤토리에서 아이템 제거
            UIManager.Instance.RemoveItemFromQuickSlot(currentSlot);
            quickSlotPrefabs[currentSlot] = null; // 프리팹 배열에서 제거
            Debug.Log($"{currentSlot + 1}번 슬롯 아이템 드랍");
        }
    }

    // 하루 넘기는 버튼에서 F키 눌렀을 때 처리
    void DayPass()
    {
        GameManager.Instance.DayPass();
        nearbyDayAdvanceTrigger = null;
        UIManager.Instance.ShowInteractionMessage("", false);
    }
}
