using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveAction; // �̵� �Է� �׼�
    public float moveSpeed = 2.0f; // �̵� �ӵ�
    public Transform playerRig; // �÷��̾��� ����(transform)

    private void Update()
    {
        Vector2 moveValue = moveAction.GetAxis(SteamVR_Input_Sources.Any);

        // �Է� ���� ������� �̵� ���� ���
        Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0; // ���� �̵��� ������ ����

        // �÷��̾� �̵�
        playerRig.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
