using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class WristAngleUI : MonoBehaviour
{
    public SteamVR_Action_Pose wristPose; // �ո� ���� �׼�
    public SteamVR_Input_Sources handType; // �Է� �ҽ� (�޼�, ������)
    public Canvas wristUICanvas; // �ո� UI ĵ����
    public Vector3 minActivationAngle = new Vector3(-60, -60, -60); // UI�� Ȱ��ȭ�� �ּ� ����
    public Vector3 maxActivationAngle = new Vector3(60, 60, 60); // UI�� Ȱ��ȭ�� �ִ� ����

    private void Start()
    {
        wristUICanvas.gameObject.SetActive(false); // ���� �� UI ��Ȱ��ȭ
    }

    private void Update()
    {
        Quaternion wristRotation = wristPose.GetLocalRotation(handType);

        // �ո��� ȸ�� ���� ��������
        Vector3 wristEulerAngles = wristRotation.eulerAngles;

        // ���� ������ üũ�Ͽ� UI Ȱ��ȭ/��Ȱ��ȭ
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
        // ������ -180 ~ 180 ������ ��ȯ
        float xAngle = eulerAngles.x > 180 ? eulerAngles.x - 360 : eulerAngles.x;
        float yAngle = eulerAngles.y > 180 ? eulerAngles.y - 360 : eulerAngles.y;
        float zAngle = eulerAngles.z > 180 ? eulerAngles.z - 360 : eulerAngles.z;

        // ���� ���� ���� �ִ��� Ȯ��
        return (xAngle >= minActivationAngle.x && xAngle <= maxActivationAngle.x) &&
               (yAngle >= minActivationAngle.y && yAngle <= maxActivationAngle.y) &&
               (zAngle >= minActivationAngle.z && zAngle <= maxActivationAngle.z);
    }
}
