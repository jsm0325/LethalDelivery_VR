using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScaler : MonoBehaviour
{
    public Transform headTracker; // �Ӹ� Ʈ��Ŀ
    public Transform footTracker; // �� Ʈ��Ŀ
    public Transform avatar; // �ƹ�Ÿ�� ��Ʈ Ʈ������

    public float userHeight = 1.75f; // ������� ���� Ű (���� ����)
    private float initialAvatarHeight; // �ʱ� �ƹ�Ÿ�� Ű

    void Start()
    {
        // �ʱ� �ƹ�Ÿ�� Ű�� ���
        initialAvatarHeight = Vector3.Distance(avatar.position, footTracker.position);
    }

    void Update()
    {
        // �Ӹ� Ʈ��Ŀ�� �� Ʈ��Ŀ ���� �Ÿ��� �����Ͽ� ���� �ƹ�Ÿ�� Ű ���
        float currentAvatarHeight = Vector3.Distance(headTracker.position, footTracker.position);

        // �ƹ�Ÿ�� �������� ������� ���� Ű�� �°� ����
        float scaleRatio = userHeight / currentAvatarHeight;
        avatar.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
    }
}

