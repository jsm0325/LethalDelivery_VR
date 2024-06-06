using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionEffect;  // 폭발 이펙트 프리팹
    public AudioClip explosionSound;    // 폭발 사운드
    public float explosionRadius = 5f;  // 폭발 범위

    private bool hasExploded = false;   // 폭발 여부 확인

    public AudioSource warningSound; // 경고음 오디오 소스 추가
    public float warningInterval = 20f; // 경고음 재생 간격
    public float intervalVariation = 5f; // 경고음 재생 간격의 무작위 변동 범위

    private void Start()
    {
        // 경고음 재생을 위한 코루틴 시작
        StartCoroutine(PlayWarningSound());
    }
    void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && other.CompareTag("Player")) // 'Player' 태그를 가진 객체와 접촉
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // 폭발 사운드 재생
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // 폭발 이펙트 생성
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // 폭발 범위 내의 모든 Collider 검색
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // 'Player' 태그를 가진 객체에 대해 즉사 처리
            if (nearbyObject.CompareTag("Player"))
            {
                // 여기서는 간단히 로그를 찍지만, 실제 게임에서는 플레이어의 사망 처리 로직을 구현
                Debug.Log("Player killed by mine");
            }
        }

        // 지뢰 객체 파괴
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // 에디터에서 폭발 범위를 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
