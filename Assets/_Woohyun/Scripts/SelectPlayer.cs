using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;

public class SelectPlayer : MonoBehaviour
{

    [SerializeField]
    private float pickupRange = 2.0f;
    [SerializeField]
    private LayerMask itemLayer;

    public Camera mainCamera;


    public SteamVR_LaserPointer steamVR_LaserPointer;
    public SteamVR_Behaviour_Pose poseBehaviour;
    public SteamVR_Action_Boolean fireAction; // ��ư �Է��� ������ SteamVR �׼�
    public SteamVR_Input_Sources handType;


    [Header("���� Ŭ��")]
    public AudioClip hoverSound;
    public AudioClip clickSound;


    private bool restart = false;
    //public AudioClip fireSound;

    void Start()
    {
        // LaserPointer �̺�Ʈ ����
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn += OnPointerIn;
            steamVR_LaserPointer.PointerClick += OnPointerClick;
        }
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
        /* �ٽ��ϱ� */
        if (e.target.gameObject.CompareTag("Restart") == true)
        {
            restart = true;
        }
        else
        {
            restart = false;
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
        else if (restart == true)
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
        GameManager.Instance.PlaySound(dayPassSound);
        nearbyDayAdvanceTrigger = null;
        UIManager.Instance.ShowInteractionMessage("", false);
    }
}
