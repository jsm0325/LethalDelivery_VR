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

    private void Start()
    {
        wristUICanvas.gameObject.SetActive(false); // 시작 시 UI 비활성화
    }

    private void Update()
    {
        Quaternion wristRotation = wristPose.GetLocalRotation(handType);

        // 손목의 회전 각도 가져오기
        Vector3 wristEulerAngles = wristRotation.eulerAngles;

        // 각도 범위를 체크하여 UI 활성화/비활성화
        if (IsActivationAngle(wristEulerAngles))
        {
            wristUICanvas.gameObject.SetActive(true);
        }
        else
        {
            wristUICanvas.gameObject.SetActive(false);
        }
    }

    private bool IsActivationAngle(Vector3 eulerAngles)
    {
        // 각도를 -180 ~ 180 범위로 변환
        float xAngle = eulerAngles.x > 180 ? eulerAngles.x - 360 : eulerAngles.x;
        float yAngle = eulerAngles.y > 180 ? eulerAngles.y - 360 : eulerAngles.y;
        float zAngle = eulerAngles.z > 180 ? eulerAngles.z - 360 : eulerAngles.z;

        // 각도 범위 내에 있는지 확인
        return (xAngle >= minActivationAngle.x && xAngle <= maxActivationAngle.x) &&
               (yAngle >= minActivationAngle.y && yAngle <= maxActivationAngle.y) &&
               (zAngle >= minActivationAngle.z && zAngle <= maxActivationAngle.z);
    }
}
