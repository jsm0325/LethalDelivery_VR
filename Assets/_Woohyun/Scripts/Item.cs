using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public string itemID { get; private set; }
    public string itemName;
    public Sprite icon;
    public int value;


    private void Awake()
    {
        itemID = Guid.NewGuid().ToString();
    }

    public void Pickup()
    {
        gameObject.SetActive(false);
    }

    public void Drop(Vector3 dropPosition)
    {
        transform.position = dropPosition;
        gameObject.SetActive(true);
    }
}
