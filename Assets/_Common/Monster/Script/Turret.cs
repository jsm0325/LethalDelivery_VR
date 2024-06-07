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
    private GameObject target;
    public float turnSpeed = 100f; // ȸ�� �ӵ�

    public AudioSource shootSound; // ���� �ҽ� �߰�
    public ParticleSystem upperMuzzleFlash; // ��� �ѱ� ȭ��
    public ParticleSystem lowerMuzzleFlash; // �ϴ� �ѱ� ȭ��

    public AudioSource warningSound; // ����� ����� �ҽ� �߰�
    public float warningInterval = 20f; // ����� ��� ����
    public float intervalVariation = 5f; // ����� ��� ������ ������ ���� ����

    public float detectionRange = 20f; // ���� ����
    public LayerMask detectionLayer; // ������ ���̾�
    public float lostTargetTimeout = 3f; // Ÿ���� ���� �� ȸ�� ���� �ð�

    private float lostTargetTimer = 0f;
    private float detectionInterval = 0.1f;
    private void Start()
    {
        // ����� ����� ���� �ڷ�ƾ ����
        StartCoroutine(PlayWarningSound());
        StartCoroutine(DetectPlayer());
    }

    void Update()
    {

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;

            // ���� �ͷ� ��尡 Z�� �������� ������ ���� �����Ƿ�, dir�� ����
            dir = Quaternion.Euler(0, 90, 0) * dir;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = targetRotation.eulerAngles;
            rotation.x = 0; // X�� ȸ�� ����
            rotation.z = 0; // Z�� ȸ�� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * 5f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {

            lostTargetTimer += Time.deltaTime;

            if (lostTargetTimer >= lostTargetTimeout)
            {
                // �⺻ ȸ�� ���·� ���ư���
                turretHead.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            Transform barrel = isUpperBarrel ? upperBarrel : lowerBarrel;
            Vector3 forward = barrel.TransformDirection(Vector3.forward);

            RaycastHit hit;
            Debug.DrawRay(barrel.position, forward * detectionRange, Color.red); // ����� ����

            if (Physics.Raycast(barrel.position, forward, out hit, detectionRange, detectionLayer))
            {
                GameObject head = GameObject.FindWithTag("MainCamera");
                if (hit.collider.CompareTag("Player"))
                {
                    
                    target = head;
                    Debug.Log("�÷��̾� ����");
                }
            }
            else
            {
                if (target != null)
                {
                    target = null;
                    Debug.Log("�÷��̾� ���� ����");
                }
            }
            yield return new WaitForSeconds(detectionInterval);
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
    IEnumerator PlayWarningSound()
    {
        while (true)
        {
            if (warningSound != null)
            {
                warningSound.Play();
            }
            float interval = warningInterval + Random.Range(-intervalVariation, intervalVariation);
            yield return new WaitForSeconds(interval);
        }
    }
}
