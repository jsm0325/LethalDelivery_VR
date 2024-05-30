using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    public string itemID { get; private set; }
    public string itemName;
    public Sprite icon;
    public int value;

    private void Awake()
    {
        if (string.IsNullOrEmpty(itemID))
        {
            itemID = Guid.NewGuid().ToString();
        }
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

    public void SetItemID(string id)
    {
        itemID = id;
    }

    public void ReturnToScene()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}
