using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("�ð� / ��¥ / ����ݾ� / ��ǥ�ݾ�")]
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI goalText;

    [Header("���� ���� �г�")]
    public GameObject gameOverPanel;

    [Header("��ȣ�ۿ� �޼���")]
    [SerializeField]
    private GameObject interactionBackground;
    [SerializeField]
    private TextMeshProUGUI interactionText;

    [Header("������ ���� UI")]
    [SerializeField]
    private GameObject itemInfoBackground;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemValueText;

    [Header("������")]
    [SerializeField]
    private Image[] slotImages;

    private Item[] quickSlotItems;

    public static UIManager Instance;

    void Awake()
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

        quickSlotItems = new Item[slotImages.Length];
    }

    private void Start()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].enabled = false;
        }
    }

    public void UpdateTimeUI(float elapsedTime, int currentDay)
    {
        float remainingTime = GameManager.Instance.dayDuration - elapsedTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = $"���� �ð�: {minutes:D2}:{seconds:D2}";

        dayText.text = $"��¥: {currentDay}";
    }

    public void UpdateMoneyUI(int currentAmount)
    {
        moneyText.text = $"���� �ݾ�: {currentAmount}��";
    }

    public void UpdateGoalUI(int currentGoalAmount)
    {
        goalText.text = $"��ǥ �ݾ�: {currentGoalAmount}��";
    }

    public void UpdateItemInfoUI(string itemName, int itemValue)
    {
        itemNameText.text = itemName;
        itemValueText.text = $"{itemValue}��";
    }

    public void ShowItemInfo(bool show)
    {
        itemInfoBackground.SetActive(show);
        itemNameText.gameObject.SetActive(show);
        itemValueText.gameObject.SetActive(show);
    }

    public void ShowGameOverUI(bool show)
    {
        gameOverPanel.SetActive(show);
    }

    public void ShowInteractionMessage(string message, bool show)
    {
        interactionBackground.SetActive(show);
        interactionText.text = message;
        interactionText.gameObject.SetActive(show);
    }

    // ������ ���
    public bool IsQuickSlotFull()
    {
        foreach (var slot in slotImages)
        {
            if (!slot.enabled)
            {
                return false;
            }
        }
        return true;
    }
    public int AddItemToQuickSlot(Item item)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (!slotImages[i].enabled)
            {
                slotImages[i].sprite = item.icon;
                slotImages[i].enabled = true;
                quickSlotItems[i] = item;
                Debug.Log($"{item.itemName} ������ {i + 1}�� ���Կ� �߰�");
                return i; // �߰��� ������ �ε��� ��ȯ
            }
        }
        return -1; // �� ������ ���� ��
    }

    public void RemoveItemFromQuickSlot(int index)
    {
        if (index >= 0 && index < slotImages.Length)
        {
            slotImages[index].sprite = null;
            slotImages[index].enabled = false;
            quickSlotItems[index] = null;
        }
    }

    public Item GetSelectedQuickSlotItem(int index)
    {
        if (index >= 0 && index < slotImages.Length && slotImages[index].enabled)
        {
            return quickSlotItems[index];
        }
        return null;
    }
}
