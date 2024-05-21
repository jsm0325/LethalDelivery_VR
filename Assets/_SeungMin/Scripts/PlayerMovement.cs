using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveAction; // 이동 입력 액션
    public float moveSpeed = 2.0f; // 이동 속도
    public Transform playerRig; // 플레이어의 리그(transform)

    private void Update()
    {
        Vector2 moveValue = moveAction.GetAxis(SteamVR_Input_Sources.Any);

        // 입력 값을 기반으로 이동 벡터 계산
        Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0; // 수직 이동은 없도록 설정

        // 플레이어 이동
        playerRig.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
