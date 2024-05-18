using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Turret : MonoBehaviour
{

    public Transform turretHead;
    public GameObject projectilePrefab;
    public float detectionRange = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public Transform firePoint;

    private Transform target;
    public float turnSpeed = 10f; // 회전 속도

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            turretHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            turretHead.Rotate(Vector3.up * turnSpeed * Time.deltaTime); // 평상시 회전
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
            fireCountdown = 0; // 타이머 초기화
        }
    }
    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
