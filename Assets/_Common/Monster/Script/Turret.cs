    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static UnityEditor.PlayerSettings;

    public class Turret : MonoBehaviour
    {
    public Transform turretHead;
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public Transform upperBarrel;
    public Transform lowerBarrel;
    private bool isUpperBarrel = true;
    private Vector3 target;
    public float turnSpeed = 100f; // 회전 속도

    public AudioSource shootSound; // 사운드 소스 추가
    public ParticleSystem upperMuzzleFlash; // 상단 총구 화염
    public ParticleSystem lowerMuzzleFlash; // 하단 총구 화염

    void Update()
    {
        if (target != Vector3.zero)
        {
            Vector3 dir = target - transform.position;

            // 현재 터렛 헤드가 Z축 기준으로 좌측을 보고 있으므로, dir을 보정
            dir = Quaternion.Euler(0, 90, 0) * dir;
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
            // 기존의 회전값을 유지하며 회전
            turretHead.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.GetComponent<CharacterController>().center;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = Vector3.zero;
            fireCountdown = 0; // 타이머 초기화
        }
    }

    void Shoot()
    {
        if (isUpperBarrel)
        {
            Instantiate(projectilePrefab, upperBarrel.position, upperBarrel.rotation);
            if (upperMuzzleFlash != null)
            {
                upperMuzzleFlash.Play();
            }
        }
        else
        {
            Instantiate(projectilePrefab, lowerBarrel.position, lowerBarrel.rotation);
            if (lowerMuzzleFlash != null)
            {
                lowerMuzzleFlash.Play();
            }
        }
        if (shootSound != null)
        {
            shootSound.Play();
        }
        isUpperBarrel = !isUpperBarrel;
    }
}
