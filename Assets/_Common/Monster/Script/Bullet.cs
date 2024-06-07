using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);  // ÃÑ¾ËÀÌ ¸ñÇ¥¹°¿¡ ´êÀ¸¸é ÆÄ±«
            Player.instance.currentHP -= 10;
        }
    }
}
