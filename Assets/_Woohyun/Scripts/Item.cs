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
    Item 처리를 하기 위한 절차
    1. Item 레이어 부여
    2. RigidBody 부여
    3. Collider 부여 (트리거 X)
*/