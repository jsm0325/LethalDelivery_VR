using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    private bool isPlayerInTrigger = false; // ���� ���� ��� �ν��Ͻ� ������ ����
    private float keyPressDuration = 0.0f;
    private float requiredPressTime = 3.0f;
    public string sceneMap = "";
    public Vector3 newPlayerPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string text = (int)(requiredPressTime - keyPressDuration + 1) + "�� �� �����մϴ�";
            UIManager.Instance.ShowInteractionMessage(text, true);
            isPlayerInTrigger = true; // �ν��Ͻ� ���� ���
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractionMessage("", false);
            isPlayerInTrigger = false; // �ν��Ͻ� ���� ���
            keyPressDuration = 0.0f; // �÷��̾ Ʈ���Ÿ� ����� �� Ű ������ ���� �ð��� ����
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            keyPressDuration += Time.deltaTime;
            if (keyPressDuration >= requiredPressTime)
            {
                // �� �̵� �� ������ ����
                InventoryManager.Instance.SaveItemData();

                SceneManager.sceneLoaded += OnSceneLoaded;
                Debug.Log(sceneMap);
                SceneManager.LoadScene(sceneMap);
                isPlayerInTrigger = false; // �ν��Ͻ� ���� ���
                UIManager.Instance.ShowInteractionMessage("", false);
            }
        }
        else
        {
            keyPressDuration = 0.0f; // Ű�� ������ ������ Ű ������ ���� �ð��� ����
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� �� Player�� ���ο� ��ġ�� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPlayerPosition;
        }

        // �� �ε� �̺�Ʈ ��� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
