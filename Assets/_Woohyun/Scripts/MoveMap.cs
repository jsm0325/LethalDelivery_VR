using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    public static bool isPlayerInTrigger = false;
    private float keyPressDuration = 0.0f;
    private float requiredPressTime = 1.0f;
    public string sceneMap = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractionMessage("F 1초, 들어가기", true);
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
        if (isPlayerInTrigger && Input.GetKey(KeyCode.F))
        {
            keyPressDuration += Time.deltaTime;
            if (keyPressDuration >= requiredPressTime)
            {
                // 씬 이동 전 데이터 저장
                InventoryManager.Instance.SaveItemData();

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
}
