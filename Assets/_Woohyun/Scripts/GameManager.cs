using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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
        UIManager.Instance.UpdateHPUI();


    }

    public void DayPass()
    {
        currentDay++;
        elapsedTime = 0; //날짜 넘어가면 경과시간 초기화

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

    public void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene("Gameover_Lethal");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("게임 오버");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
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

        InventoryManager.Instance.ClearItemData(); // JSON 데이터 초기화
        InventoryManager.Instance.RestoreItems();
    }

    public void ReStartNewGame()
    {
        currentDay = 1;
        currentGoalAmount = startGoalAmount;
        currentValue = 0;
        elapsedTime = 0;
        isGameOver = false;

        UIManager.Instance.UpdateGoalUI(currentGoalAmount);
        UIManager.Instance.UpdateMoneyUI(currentValue);
        UIManager.Instance.ShowGameOverUI(false);

        InventoryManager.Instance.ClearItemData(); // JSON 데이터 초기화
        InventoryManager.Instance.InitializeAllItemData(); 
        InventoryManager.Instance.RestoreItems();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene("OutMap");
        player.transform.position = new Vector3(346, 0.14f, 472.085f);
    }
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
