using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("첫 목표 금액")]
    public int startGoalAmount = 500; 
    [Header("목표금액 증가치 (범위랜덤 할 수 있을듯)")]
    public int goalIncrease = 500;
    [Header("목표당 일수")]
    public int daysPerGoal = 3;
    [Header("하루의 길이")]
    public float dayDuration = 20 * 60; 

    private int currentDay = 1; // 현재 날짜
    private int currentGoalAmount;
    private float elapsedTime = 0;

    private int currentValue = 0; // 현재 금액
    public bool isGameOver = false; // 게임 오버 상태

    public static GameManager Instance;

    void Start()
    {
        Instance = this;
        currentGoalAmount = startGoalAmount;
        UIManager.Instance.UpdateGoalUI(currentGoalAmount);
        UIManager.Instance.UpdateMoneyUI(currentValue);
        UIManager.Instance.ShowGameOverUI(false);

        //마우스 커서 안보이게
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        /* 게임오버시에는 Update 함수를 막음 */
        if (isGameOver)
        {
            return;
        }

        elapsedTime += Time.deltaTime; //경과시간
        if (elapsedTime >= dayDuration)
        {
            DayPass();
        }

        UIManager.Instance.UpdateTimeUI(elapsedTime, currentDay);

        //왼쪽ALT키를 눌러 마우스 커서 보이게
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    /* 하루 지나가는 로직
     * 추가해야할 것
     * 1. 아이템 랜덤 스폰 배치
     * 2. 하루가 끝나면 이펙트나 알림?
     * 3. 기타 등등..
     */
    public void DayPass()
    {
        currentDay++;
        elapsedTime = -1; //날짜 넘어가면 경과시간 초기화

        if ((currentDay - 1) % daysPerGoal == 0) // 목표 달성 기간이 끝나면 목표 금액 검사
        {
            //if 3일차가 지났을 때 현재금액이 목표금액에 도달하지 못했을 때 GameOver 처리
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

    /* 캡슐화인데, 그냥 static 처리해도 될듯 */
    public int GetCurrentGoalAmount()
    {
        return currentGoalAmount;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }



    /* 게임 오버 처리
     * 추가되어야할 것
     * 1. 게임 다시시작 할 수 있는 로직
     * 2. 게임 다시시작하면 현재금 / 경과 날짜 / 아이템 등 초기화되어야 함
     */ 
    private void GameOver()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI(true);
        Debug.Log("게임 오버");
    }
}
