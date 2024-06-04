using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().hp -= 1.0f;
            print("enemy");
        }
    }
}
    