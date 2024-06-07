using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    public static bool isPlayerInTrigger = false;
    private float keyPressDuration = 0.0f;
    private float requiredPressTime = 3.0f;
    public string sceneMap = "";
    public Vector3 newPlayerPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string text = (int)(requiredPressTime - keyPressDuration + 1) + "초 후 입장합니다";
            UIManager.Instance.ShowInteractionMessage(text, true);
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractionMessage("", false);
            isPlayerInTrigger = false;
            keyPressDuration = 0.0f; // Reset the key press duration when the player exits the trigger
        }
    } 

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            keyPressDuration += Time.deltaTime;
            if (keyPressDuration >= requiredPressTime)
            {
                // 씬 이동 전 데이터 저장
                InventoryManager.Instance.SaveItemData();

                SceneManager.sceneLoaded += OnSceneLoaded;

                SceneManager.LoadScene(sceneMap);
                isPlayerInTrigger = false;
                UIManager.Instance.ShowInteractionMessage("", false);
            }
        }
        else
        {
            keyPressDuration = 0.0f; // Reset the key press duration if the key is not held
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 Player의 새로운 위치를 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPlayerPosition;
        }

        // 씬 로드 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
