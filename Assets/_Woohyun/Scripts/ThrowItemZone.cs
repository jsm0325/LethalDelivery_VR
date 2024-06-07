using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItemZone : MonoBehaviour
{

    public AudioClip sellSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Item itemComponent = other.GetComponent<Item>();
            GameManager.Instance.PlaySound(sellSound);
            Debug.Log("Ʈ���� ����");
            
            if (itemComponent != null)
            {
                int increaseValue = itemComponent.value;

                // ���� ��¥�� 3���� �������� �ʴ� ���̸� �Ǹ� �ݾ� 50% ����
                // 4������ �Ѿ�� �� ������ ���� �ƴϸ� �Ǹ� �ݾ��� �����ϴ°�
                if (GameManager.Instance.GetCurrentDay() % 3 != 0)
                {
                    increaseValue = Mathf.FloorToInt(increaseValue * 0.5f);
                }
                GameManager.Instance.IncreaseCurrentValue(increaseValue);
                InventoryManager.Instance.RemoveItemData(itemComponent.itemID);
                other.gameObject.SetActive(false);
            }
        }
    }
}
