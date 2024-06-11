using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().hp -= 1.0f;
            print("enemy");

            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
