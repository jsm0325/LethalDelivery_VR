using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

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
    //private Camera mainCamera;
    private int currentSlot = 0;

    //체력
    private int maxHP = 100;
    public int currentHP = 100;

    private static Player instance;
    public SteamVR_LaserPointer steamVR_LaserPointer;
    public SteamVR_Behaviour_Pose poseBehaviour;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //mainCamera = Camera.main;
        UIManager.Instance.ShowItemInfo(false);
        InventoryManager.Instance.RestoreItems();
        // LaserPointer 이벤트 구독
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn += OnPointerIn;
            steamVR_LaserPointer.PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn -= OnPointerIn;
            steamVR_LaserPointer.PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        Item item = e.target.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
            UIManager.Instance.UpdateItemInfoUI(item.itemName, item.value);
            UIManager.Instance.ShowItemInfo(true);
        }
        else
        {
            nearbyItem = null;
            UIManager.Instance.ShowItemInfo(false);
        }
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if (nearbyItem != null)
        {
            PickupItem();
        }
    }

    private void OnPoseUpdated(SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == SteamVR_Input_Sources.RightHand) // 오른손 컨트롤러에서 발생한 이벤트인지 확인
        {
            Debug.Log("Right hand pose updated");
            // 여기에서 원하는 동작을 추가합니다.
        }
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        
        ///CheckForItem();
//CheckForDayAdvanceTrigger();

        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F)) { PickupItem(); }
        if (nearbyDayAdvanceTrigger != null && Input.GetKeyDown(KeyCode.F)) { DayPass(); }
        //if (Input.GetKeyDown(KeyCode.G)) { DropItem(); }

        //퀵슬롯
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectSlot(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectSlot(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectSlot(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectSlot(3); }
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
   
    /*void CheckForItem()
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
    }*/

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
                InventoryManager.Instance.AddItem(nearbyItem); // 인벤토리에 아이템 추가
                nearbyItem.Pickup(); // 아이템 오브젝트 비활성화
                nearbyItem = null;
                UIManager.Instance.ShowItemInfo(false);
            }
        }
    }
    /*
    void DropItem()
    {
        Item itemToDrop = UIManager.Instance.GetSelectedQuickSlotItem(currentSlot);
        if (itemToDrop != null)
        {
            Vector3 dropPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
            itemToDrop.Drop(dropPosition);
            InventoryManager.Instance.RemoveItem(itemToDrop); // 인벤토리에서 아이템 제거 및 씬으로 반환
            UIManager.Instance.RemoveItemFromQuickSlot(currentSlot);
            Debug.Log($"{currentSlot + 1}번 슬롯 아이템 드랍");
        }
    }*/

    // 하루 넘기는 버튼에서 F키 눌렀을 때 처리
    void DayPass()
    {
        GameManager.Instance.DayPass();
        nearbyDayAdvanceTrigger = null;
        UIManager.Instance.ShowInteractionMessage("", false);
    }
}