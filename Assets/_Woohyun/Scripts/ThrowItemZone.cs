using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ThrowItemZone은 회사 건물에 배치하면 될듯
 */

public class ThrowItemZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Item itemComponent = other.GetComponent<Item>();
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
