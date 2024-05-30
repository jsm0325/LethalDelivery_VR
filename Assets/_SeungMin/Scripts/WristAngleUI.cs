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
    public Vector3 offset = new Vector3(0, 0.1f, 0); // �ո� ���� UI�� ��ġ��Ű�� ���� ������
    public float smoothSpeed = 5f; // UI�� �ո��� ���󰡴� �ӵ�
    public Transform wristTransform; // �ո��� Transform
    public Transform cameraTransform; // ī�޶��� Transform

    private void Start()
    {
        wristUICanvas.gameObject.SetActive(false); // ���� �� UI ��Ȱ��ȭ
    }

    private void Update()
    {
        IsActivationAngle();

        wristUICanvas.transform.LookAt(cameraTransform);

        if (wristTransform != null)
        {
            // �ո��� ��ġ�� ȸ���� ������� UI�� ��ġ�� ȸ���� ����
            Vector3 targetPosition = wristTransform.position + wristTransform.TransformVector(offset);
            wristUICanvas.transform.position = Vector3.Lerp(wristUICanvas.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private void IsActivationAngle()
    {
        Quaternion handRotation = wristPose.GetLocalRotation(handType);

        float zAngle = handRotation.eulerAngles.z;

        // �ո��� ������ �� UI Ȱ��ȭ
        if (zAngle > 200f && zAngle < 300f && !wristUICanvas.gameObject.activeSelf)
        {
            wristUICanvas.gameObject.SetActive(true);
        }

        // �ո��� ���� ��ġ�� �ǵ����� �� UI ��Ȱ��ȭ
        if ((zAngle <= 200f || zAngle >= 300f) && wristUICanvas.gameObject.activeSelf)
        {
            wristUICanvas.gameObject.SetActive(false);
        }
    }
}
