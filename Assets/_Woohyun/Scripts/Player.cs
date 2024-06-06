using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    private GameObject nearbyQuickSlot;
    private int currentSlot = 0;
    //private Camera mainCamera;
    public Camera mainCamera;



    //ü��
    private int maxHP = 100;
    public int currentHP = 100;
    private int quickSlotIndex = -1;

    public SteamVR_LaserPointer steamVR_LaserPointer;
    public SteamVR_Behaviour_Pose poseBehaviour;
    public SteamVR_Action_Boolean fireAction; // ��ư �Է��� ������ SteamVR �׼�
    public SteamVR_Input_Sources handType;

    public FireBullet FirebBullet;




    public AudioClip pickupSound;
    public AudioClip dropSound;
    public AudioClip quickSlotfullSound;
    int restart = -1;
    //public AudioClip fireSound;

    public static Player instance;

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
        // LaserPointer �̺�Ʈ ����
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn += OnPointerIn;
            steamVR_LaserPointer.PointerClick += OnPointerClick;
        }

        fireAction.AddOnStateDownListener(OnFireAction, handType);

    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn -= OnPointerIn;
            steamVR_LaserPointer.PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        /* ������ �Ⱦ� */
        Item item = e.target.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
            nearbyQuickSlot = null;
            UIManager.Instance.UpdateItemInfoUI(item.itemName, item.value);
            UIManager.Instance.ShowItemInfo(true);
        }
        else
        {
            nearbyItem = null;
            UIManager.Instance.ShowItemInfo(false);
        }

        /* ������ ��� */
        quickSlotIndex = UIManager.Instance.GetSlotGameObjectIndex(e.target.gameObject);            
        nearbyQuickSlot = e.target.gameObject;

        /* ��¥ �ѱ� */
        DayPassTrigger dayPassTrigger = e.target.GetComponent<DayPassTrigger>();
        if (dayPassTrigger != null)
        {
            nearbyDayAdvanceTrigger = dayPassTrigger;
            UIManager.Instance.ShowInteractionMessage("��¥ �ѱ�", true);
        }
        else
        {
            nearbyDayAdvanceTrigger = null;
            UIManager.Instance.ShowInteractionMessage("", false);
        }

        /* �ٽ��ϱ� */
        if (e.target.gameObject.CompareTag("Restart") == true)
        {
            restart = 1;
        }
        else
        {
            restart = -1;
        }

    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if (nearbyItem != null)
        {
            PickupItem();
        }
        else if (quickSlotIndex != -1)
        {
            DropItem(quickSlotIndex);
        }
        else if (nearbyDayAdvanceTrigger != null)
        {
            DayPass();
        }
        else if (restart == 1)
        {
            GameManager.Instance.ReStartNewGame();
        }
    }

    private void OnFireAction(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == SteamVR_Input_Sources.RightHand) // ������ ��Ʈ�ѷ����� �߻��� �̺�Ʈ���� Ȯ��
        {
            FirebBullet.Shoot();
        }
    }

    void Update()
    {
        Debug.Log(restart);
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        //CheckForDayAdvanceTrigger();

       if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }

    }

    void PickupItem()
    {
        if (nearbyItem != null)
        {
            if (UIManager.Instance.IsQuickSlotFull())
            {
                GameManager.Instance.PlaySound(quickSlotfullSound);
                Debug.Log("������ �� ��");
                return;
            }

            int slotIndex = UIManager.Instance.AddItemToQuickSlot(nearbyItem);
            if (slotIndex != -1)
            {
                GameManager.Instance.PlaySound(pickupSound);
                InventoryManager.Instance.AddItem(nearbyItem); // �κ��丮�� ������ �߰�
                nearbyItem.Pickup(); // ������ ������Ʈ ��Ȱ��ȭ
                nearbyItem = null;
                UIManager.Instance.ShowItemInfo(false);
            }
        }
    }

    void DropItem(int slotIndex)
    {
        Item itemToDrop = UIManager.Instance.GetSelectedQuickSlotItem(slotIndex);
        if (itemToDrop != null)
        {
            GameManager.Instance.PlaySound(dropSound);
            Vector3 dropPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
            itemToDrop.Drop(dropPosition);
            InventoryManager.Instance.RemoveItem(itemToDrop); // �κ��丮���� ������ ���� �� ������ ��ȯ
            UIManager.Instance.RemoveItemFromQuickSlot(slotIndex);
            Debug.Log($"{slotIndex + 1}�� ���� ������ ���");
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
