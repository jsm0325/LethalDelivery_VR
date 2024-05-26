using TMPro;
using UnityEngine;

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

    [Header("하루 넘기는 버튼 상호작용 메세지")]
    [SerializeField]
    private GameObject interactionBackground;
    [SerializeField]
    private TextMeshProUGUI interactionText;

    //아이템 정보 표시
    [Header("아이템 정보 UI")]
    [SerializeField]
    private GameObject itemInfoBackground;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemValueText;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        itemValueText.text = itemValue.ToString() + "원";
    }

    public void ShowItemInfo(bool show)
    {
        itemInfoBackground.gameObject.SetActive(show);
        itemNameText.gameObject.SetActive(show);
        itemValueText.gameObject.SetActive(show);
    }

    public void ShowGameOverUI(bool show)
    {
        gameOverPanel.SetActive(show);
    }

    public void ShowInteractionMessage(string message, bool show)
    {
        interactionBackground.gameObject.SetActive(show);
        interactionText.text = message;
        interactionText.gameObject.SetActive(show);
    }
}
