using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("시간 / 날짜 / 현재금액 / 목표금액")]
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI goalText;

    [Header("게임 오버 패널")]
    public GameObject gameOverPanel;

    [Header("상호작용 메세지")]
    [SerializeField]
    private GameObject interactionBackground;
    [SerializeField]
    private TextMeshProUGUI interactionText;

    [Header("아이템 정보 UI")]
    [SerializeField]
    private GameObject itemInfoBackground;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemValueText;

    [Header("퀵슬롯")]
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
        timeText.text = $"남은 시간: {minutes:D2}:{seconds:D2}";

        dayText.text = $"날짜: {currentDay}";
    }

    public void UpdateMoneyUI(int currentAmount)
    {
        moneyText.text = $"현재 금액: {currentAmount}원";
    }

    public void UpdateGoalUI(int currentGoalAmount)
    {
        goalText.text = $"목표 금액: {currentGoalAmount}원";
    }

    public void UpdateItemInfoUI(string itemName, int itemValue)
    {
        itemNameText.text = itemName;
        itemValueText.text = $"{itemValue}원";
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

    // 퀵슬롯 기능
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
                Debug.Log($"{item.itemName} 아이템 {i + 1}번 슬롯에 추가");
                return i; // 추가된 슬롯의 인덱스 반환
            }
        }
        return -1; // 빈 슬롯이 없을 때
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
