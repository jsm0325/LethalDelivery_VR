using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public static int CurrentValue = 0;

    [SerializeField]
    private TextMeshProUGUI value;

    void Start()
    {
        //���콺 Ŀ�� �Ⱥ��̰�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        value.text = CurrentValue.ToString();

        //���콺 Ŀ�� ���̰�
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
