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

    public AudioSource warningSound; // 경고음 오디오 소스 추가
    public float warningInterval = 20f; // 경고음 재생 간격
    public float intervalVariation = 5f; // 경고음 재생 간격의 무작위 변동 범위

    public float detectionRange = 20f; // 감지 범위
    public LayerMask detectionLayer; // 감지할 레이어
    public float lostTargetTimeout = 3f; // 타겟을 잃은 후 회전 시작 시간

    private float lostTargetTimer = 0f;

    private void Start()
    {
        // 경고음 재생을 위한 코루틴 시작
        StartCoroutine(PlayWarningSound());
    }

    void Update()
    {

        DetectPlayer();

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

            lostTargetTimer += Time.deltaTime;

            if (lostTargetTimer >= lostTargetTimeout)
            {
                // 기본 회전 상태로 돌아가기
                turretHead.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }
        }
    }

    void DetectPlayer()
    {
        Transform barrel = isUpperBarrel ? upperBarrel : lowerBarrel;
        Vector3 forward = barrel.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Debug.DrawRay(barrel.position, forward * detectionRange, Color.red); // 디버그 레이

        if (Physics.Raycast(barrel.position, forward, out hit, detectionRange, detectionLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                target = hit.transform.gameObject.GetComponent<CharacterController>().center;
                Debug.Log("플레이어 감지");
            }
        }
        else
        {
            if (target != Vector3.zero)
            {
                target = Vector3.zero;
                Debug.Log("플레이어 감지 실패");
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
