using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static int currentScore;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public Slider HPSlider;
    public TextMeshProUGUI HPText;

    [Header("하루의 길이")]
    public float dayDuration = 5 * 60;
    private float elapsedTime = 0;

    public bool isGameOver = false; // 게임 오버 상태


    public static GameManager2 Instance;
    public Player player;

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

        elapsedTime += Time.deltaTime; //경과시간
        if (elapsedTime >= dayDuration)
        {
            GameOver();
        }

        float remainingTime = GameManager2.Instance.dayDuration - elapsedTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = $"남은 시간: {minutes:D2}:{seconds:D2}";

        scoreText.text = "스코어 " + (currentScore * 100).ToString(); //score

        float targetValue = player.currentHP; // Target slider value

        // Smoothly interpolate between current and target value
        float lerpSpeed = 5f; // Adjust this value to control animation speed
        HPSlider.value = Mathf.Lerp(HPSlider.value, targetValue, Time.deltaTime * lerpSpeed);

        // Adjust lerpSpeed to control the animation speed

        HPText.text = player.currentHP.ToString() + "%";
    }


    public void IncreaseCurrentValue(int amount)
    {
        currentScore += amount;
        //UIManager.Instance.UpdateMoneyUI(currentValue);
    }


    private void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene("Gameover");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("게임 오버");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 Player의 새로운 위치를 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0,0,0);
        }

        // 씬 로드 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartNewGame()
    {
        //currentDay = 1;
        //currentGoalAmount = startGoalAmount;
        //currentValue = 0;
        elapsedTime = 0;
        isGameOver = false;

        //UIManager.Instance.UpdateGoalUI(currentGoalAmount);
        //UIManager.Instance.UpdateMoneyUI(currentValue);
        UIManager.Instance.ShowGameOverUI(false);

        InventoryManager.Instance.ClearItemData(); // JSON 데이터 초기화
        InventoryManager.Instance.RestoreItems();
    }
}
