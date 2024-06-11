using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;

    public float bulletSpeed = 20f;
    public AudioClip shootSound;

    public void Shoot()
    {
        GameManager2.Instance.PlaySound(shootSound);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = muzzle.forward * bulletSpeed;
        }
    }
}
