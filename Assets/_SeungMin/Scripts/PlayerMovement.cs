using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class PlayerMovement : MonoBehaviour
{
    
    public float m_Sensitivity = 0.1f; // �̵� �ΰ���
    public float m_Speed = 0f; // �̵� �ӵ�
    public float m_MaxSpeed = 1.0f;
    public float m_RotateIncrement = 90;
    public float m_Gravity = 30.0f;

    public SteamVR_Action_Boolean m_RotatePress = null;
    public SteamVR_Action_Boolean m_MovePress = null; // �̵� ��ư �׼�
    public SteamVR_Action_Vector2 m_MoveValue = null; // �̵� �Է� �׼�

    private CharacterController m_CharacterController = null; // ĳ���� ��Ʈ�ѷ�
    private Transform m_CameraRig = null; // �÷��̾��� ����(transform)
    private Transform m_head; // �÷��̾��� �Ӹ�(transform)
    private Vector3 lastHeadPosition; // �ʱ� ī�޶� ���� ��ġ

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_CameraRig = SteamVR_Render.Top().origin;
        m_head = SteamVR_Render.Top().head;
        m_CharacterController.transform.position = m_head.position;
        // �÷��̾� ������ �ʱ� ��ġ ����
        if (m_CameraRig != null && m_head != null)
        {
            lastHeadPosition = m_head.position; // �ʱ� ī�޶� ���� ��ġ ����
        }
        
    }

    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapRotation();
    }



    private void CalculateMovement()
    {
        // �̵� ���� ���
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // �̵����� �ʴ� ���
        if (m_MoveValue.axis.magnitude == 0)
        {
            m_Speed = 0;
        }

        // ��ư�� ���� ���
        m_Speed += m_MoveValue.axis.magnitude * m_Sensitivity;
        m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);


        movement += orientation * (m_Speed * Vector3.forward);
        movement.y -= m_Gravity * Time.deltaTime;

        // �߷�
        m_CharacterController.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, m_head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

    private void HandleHeight()
    {
        // Get head in local space
        float headHeight = Mathf.Clamp(m_head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        // Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        // Apply
        m_CharacterController.center = newCenter;
    }

    private void SnapRotation()
    {
        float snapValue = 0.0f;
        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
            snapValue = -Mathf.Abs(m_RotateIncrement);
        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
            snapValue = -Mathf.Abs(m_RotateIncrement);

        transform.RotateAround(m_head.position, Vector3.up, snapValue);
    }
}
