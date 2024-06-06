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
    private GameObject[] slotGameObjects;
    [SerializeField]
    private Item[] quickSlotItems;

    [Header("체력")]
    [SerializeField]
    private Slider HPSlider;
    [SerializeField]
    private TextMeshProUGUI HPText;

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

        quickSlotItems = new Item[slotGameObjects.Length];
    }

    private void Start()
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            slotGameObjects[i].GetComponent<Image>().enabled = false;
        }
    }

    public void UpdateHPUI()
    {
        HPSlider.value = Player.instance.currentHP;
        HPText.text = Player.instance.currentHP.ToString() + "%";
    }

    public void UpdateTimeUI(float elapsedTime, int currentDay)
    {
        float remainingTime = GameManager.Instance.dayDuration - elapsedTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        dayText.text = $"현재 {currentDay}일 생존";
    }

    public void UpdateMoneyUI(int currentAmount)
    {
        moneyText.text = $"현재 {currentAmount}룬";
    }

    public void UpdateGoalUI(int currentGoalAmount)
    {
        goalText.text = $"목표 {currentGoalAmount}룬";
    }

    public void UpdateItemInfoUI(string itemName, int itemValue)
    {
        itemNameText.text = itemName;
        itemValueText.text = $"{itemValue}룬";
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
        foreach (var slot in slotGameObjects)
        {
            if (!slot.GetComponent<Image>().enabled)
            {
                return false;
            }
        }
        return true;
    }

    public int AddItemToQuickSlot(Item item)
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            Image slotImage = slotGameObjects[i].GetComponent<Image>();
            if (!slotImage.enabled)
            {
                slotImage.sprite = item.icon;
                slotImage.enabled = true;
                quickSlotItems[i] = item;
                Debug.Log($"{item.itemName} 아이템 {i + 1}번 슬롯에 추가");
                return i; // 추가된 슬롯의 인덱스 반환
            }
        }
        return -1; // 빈 슬롯이 없을 때
    }

    public void RemoveItemFromQuickSlot(int index)
    {
        if (index >= 0 && index < slotGameObjects.Length)
        {
            slotGameObjects[index].GetComponent<Image>().sprite = null;
            slotGameObjects[index].GetComponent<Image>().enabled = false;
            quickSlotItems[index] = null;
        }
    }

    public Item GetSelectedQuickSlotItem(int index)
    {
        if (index >= 0 && index < slotGameObjects.Length && slotGameObjects[index].GetComponent<Image>().enabled)
        {
            return quickSlotItems[index];
        }
        return null;
    }

    // 슬롯 이미지의 인덱스를 가져오는 메서드
    public int GetSlotGameObjectIndex(GameObject slotGameObject)
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            if (slotGameObjects[i] == slotGameObject)
            {
                return i;
            }
        }
        return -1;
    }
}
