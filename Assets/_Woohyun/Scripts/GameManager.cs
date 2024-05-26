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

    void Start()
    {
        Instance = this;
        currentGoalAmount = startGoalAmount;
        UIManager.Instance.UpdateGoalUI(currentGoalAmount);
        UIManager.Instance.UpdateMoneyUI(currentValue);
        UIManager.Instance.ShowGameOverUI(false);

        //���콺 Ŀ�� �Ⱥ��̰�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        /* ���ӿ����ÿ��� Update �Լ��� ���� */
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

        //����ALTŰ�� ���� ���콺 Ŀ�� ���̰�
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    /* �Ϸ� �������� ����
     * �߰��ؾ��� ��
     * 1. ������ ���� ���� ��ġ
     * 2. �Ϸ簡 ������ ����Ʈ�� �˸�?
     * 3. ��Ÿ ���..
     */
    public void DayPass()
    {
        currentDay++;
        elapsedTime = -1; //��¥ �Ѿ�� ����ð� �ʱ�ȭ

        if ((currentDay - 1) % daysPerGoal == 0) // ��ǥ �޼� �Ⱓ�� ������ ��ǥ �ݾ� �˻�
        {
            //if 3������ ������ �� ����ݾ��� ��ǥ�ݾ׿� �������� ������ �� GameOver ó��
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

    /* ĸ��ȭ�ε�, �׳� static ó���ص� �ɵ� */
    public int GetCurrentGoalAmount()
    {
        return currentGoalAmount;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }



    /* ���� ���� ó��
     * �߰��Ǿ���� ��
     * 1. ���� �ٽý��� �� �� �ִ� ����
     * 2. ���� �ٽý����ϸ� ����� / ��� ��¥ / ������ �� �ʱ�ȭ�Ǿ�� ��
     */ 
    private void GameOver()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI(true);
        Debug.Log("���� ����");
    }
}
