using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("ù ��ǥ �ݾ�")]
    public int startGoalAmount = 500; 
    [Header("��ǥ�ݾ� ����ġ (�������� �� �� ������)")]
    public int goalIncrease = 500;
    [Header("��ǥ�� �ϼ�")]
    public int daysPerGoal = 3;
    [Header("�Ϸ��� ����")]
    public float dayDuration = 20 * 60; 

    private int currentDay = 1; // ���� ��¥
    private int currentGoalAmount;
    private float elapsedTime = 0;

    private int currentValue = 0; // ���� �ݾ�
    public bool isGameOver = false; // ���� ���� ����

    public static GameManager Instance;

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
    }

    void Start()
    {
        StartNewGame();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        elapsedTime += Time.deltaTime; //����ð�
        if (elapsedTime >= dayDuration)
        {
            DayPass();
        }

        UIManager.Instance.UpdateTimeUI(elapsedTime, currentDay);
        UIManager.Instance.UpdateHPUI();


    }

    public void DayPass()
    {
        currentDay++;
        elapsedTime = 0; //��¥ �Ѿ�� ����ð� �ʱ�ȭ

        if ((currentDay - 1) % daysPerGoal == 0)
        {
            if (currentValue < currentGoalAmount)
            {
                GameOver();
            }
            else
            {
                currentGoalAmount += goalIncrease;
                UIManager.Instance.UpdateGoalUI(currentGoalAmount);
            }
        }
    }

    public void IncreaseCurrentValue(int amount)
    {
        currentValue += amount;
        UIManager.Instance.UpdateMoneyUI(currentValue);
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    private void GameOver()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI(true);
        Debug.Log("���� ����");
    }

    public void StartNewGame()
    {
        currentDay = 1;
        currentGoalAmount = startGoalAmount;
        currentValue = 0;
        elapsedTime = 0;
        isGameOver = false;

        UIManager.Instance.UpdateGoalUI(currentGoalAmount);
        UIManager.Instance.UpdateMoneyUI(currentValue);
        UIManager.Instance.ShowGameOverUI(false);

        InventoryManager.Instance.ClearItemData(); // JSON ������ �ʱ�ȭ
        InventoryManager.Instance.RestoreItems();
    }
}
