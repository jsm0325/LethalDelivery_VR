using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform muzzle;

    private float destroyTime = 3f;
    public float bulletSpeed = 20f;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = muzzle.forward * bulletSpeed;
            destroyTime = 3;
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)
            {
                Destroy(bullet);
            }
        }
    }
}
