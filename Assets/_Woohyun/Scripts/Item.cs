using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public int value;



    public void Pickup()
    {
        gameObject.SetActive(false);

    }

}
/*
    Item ó���� �ϱ� ���� ����
    1. Item ���̾� �ο�
    2. RigidBody �ο�
    3. Collider �ο� (Ʈ���� X)
*/