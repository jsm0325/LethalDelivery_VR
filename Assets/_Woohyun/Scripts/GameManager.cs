using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene("Gameover_Lethal");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("���� ����");
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

        InventoryManager.Instance.ClearItemData(); // JSON ������ �ʱ�ȭ
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

        InventoryManager.Instance.ClearItemData(); // JSON ������ �ʱ�ȭ
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
