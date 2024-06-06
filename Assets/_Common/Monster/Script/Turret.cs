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
    public float turnSpeed = 100f; // ȸ�� �ӵ�

    public AudioSource shootSound; // ���� �ҽ� �߰�
    public ParticleSystem upperMuzzleFlash; // ��� �ѱ� ȭ��
    public ParticleSystem lowerMuzzleFlash; // �ϴ� �ѱ� ȭ��

    void Update()
    {
        if (target != Vector3.zero)
        {
            Vector3 dir = target - transform.position;

            // ���� �ͷ� ��尡 Z�� �������� ������ ���� �����Ƿ�, dir�� ����
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
            // ������ ȸ������ �����ϸ� ȸ��
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
            fireCountdown = 0; // Ÿ�̸� �ʱ�ȭ
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
