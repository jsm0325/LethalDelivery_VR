using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                ValueManager.CurrentValue += itemComponent.value;
                other.gameObject.SetActive(false);
            }
        }
    }
}
