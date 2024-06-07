using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    private bool isPlayerInTrigger = false; // 정적 변수 대신 인스턴스 변수로 변경
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
            isPlayerInTrigger = true; // 인스턴스 변수 사용
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractionMessage("", false);
            isPlayerInTrigger = false; // 인스턴스 변수 사용
            keyPressDuration = 0.0f; // 플레이어가 트리거를 벗어났을 때 키 프레스 지속 시간을 리셋
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
                Debug.Log(sceneMap);
                SceneManager.LoadScene(sceneMap);
                isPlayerInTrigger = false; // 인스턴스 변수 사용
                UIManager.Instance.ShowInteractionMessage("", false);
            }
        }
        else
        {
            keyPressDuration = 0.0f; // 키가 눌리지 않으면 키 프레스 지속 시간을 리셋
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
