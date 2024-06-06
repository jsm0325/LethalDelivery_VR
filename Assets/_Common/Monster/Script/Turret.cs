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

    public AudioSource warningSound; // ����� ����� �ҽ� �߰�
    public float warningInterval = 20f; // ����� ��� ����
    public float intervalVariation = 5f; // ����� ��� ������ ������ ���� ����

    public float detectionRange = 20f; // ���� ����
    public LayerMask detectionLayer; // ������ ���̾�
    public float lostTargetTimeout = 3f; // Ÿ���� ���� �� ȸ�� ���� �ð�

    private float lostTargetTimer = 0f;

    private void Start()
    {
        // ����� ����� ���� �ڷ�ƾ ����
        StartCoroutine(PlayWarningSound());
    }

    void Update()
    {

        DetectPlayer();

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

            lostTargetTimer += Time.deltaTime;

            if (lostTargetTimer >= lostTargetTimeout)
            {
                // �⺻ ȸ�� ���·� ���ư���
                turretHead.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }
        }
    }

    void DetectPlayer()
    {
        Transform barrel = isUpperBarrel ? upperBarrel : lowerBarrel;
        Vector3 forward = barrel.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Debug.DrawRay(barrel.position, forward * detectionRange, Color.red); // ����� ����

        if (Physics.Raycast(barrel.position, forward, out hit, detectionRange, detectionLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                target = hit.transform.gameObject.GetComponent<CharacterController>().center;
                Debug.Log("�÷��̾� ����");
            }
        }
        else
        {
            if (target != Vector3.zero)
            {
                target = Vector3.zero;
                Debug.Log("�÷��̾� ���� ����");
            }
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
