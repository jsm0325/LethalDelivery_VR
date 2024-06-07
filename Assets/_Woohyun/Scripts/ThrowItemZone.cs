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
            Debug.Log("트리거 감지");
            
            if (itemComponent != null)
            {
                int increaseValue = itemComponent.value;

                // 현재 날짜가 3으로 떨어지지 않는 날이면 판매 금액 50% 감소
                // 4일차로 넘어가기 전 마지막 날이 아니면 판매 금액이 감소하는것
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
