using TMPro;
using UnityEngine;

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

    [Header("�Ϸ� �ѱ�� ��ư ��ȣ�ۿ� �޼���")]
    [SerializeField]
    private GameObject interactionBackground;
    [SerializeField]
    private TextMeshProUGUI interactionText;

    //������ ���� ǥ��
    [Header("������ ���� UI")]
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
        itemValueText.text = itemValue.ToString() + "��";
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
