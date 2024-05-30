using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class WristAngleUI : MonoBehaviour
{
    public SteamVR_Action_Pose wristPose; // 손목 포즈 액션
    public SteamVR_Input_Sources handType; // 입력 소스 (왼손, 오른손)
    public Canvas wristUICanvas; // 손목 UI 캔버스
    public Vector3 minActivationAngle = new Vector3(-60, -60, -60); // UI를 활성화할 최소 각도
    public Vector3 maxActivationAngle = new Vector3(60, 60, 60); // UI를 활성화할 최대 각도
    public Vector3 offset = new Vector3(0, 0.1f, 0); // 손목 위에 UI를 위치시키기 위한 오프셋
    public float smoothSpeed = 5f; // UI가 손목을 따라가는 속도
    public Transform wristTransform; // 손목의 Transform
    public Transform cameraTransform; // 카메라의 Transform

    private void Start()
    {
        wristUICanvas.gameObject.SetActive(false); // 시작 시 UI 비활성화
    }

    private void Update()
    {
        IsActivationAngle();

        wristUICanvas.transform.LookAt(cameraTransform);

        if (wristTransform != null)
        {
            // 손목의 위치와 회전을 기반으로 UI의 위치와 회전을 설정
            Vector3 targetPosition = wristTransform.position + wristTransform.TransformVector(offset);
            wristUICanvas.transform.position = Vector3.Lerp(wristUICanvas.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private void IsActivationAngle()
    {
        Quaternion handRotation = wristPose.GetLocalRotation(handType);

        float zAngle = handRotation.eulerAngles.z;

        // 손목을 돌렸을 때 UI 활성화
        if (zAngle > 200f && zAngle < 300f && !wristUICanvas.gameObject.activeSelf)
        {
            wristUICanvas.gameObject.SetActive(true);
        }

        // 손목을 원래 위치로 되돌렸을 때 UI 비활성화
        if ((zAngle <= 200f || zAngle >= 300f) && wristUICanvas.gameObject.activeSelf)
        {
            wristUICanvas.gameObject.SetActive(false);
        }
    }
}
