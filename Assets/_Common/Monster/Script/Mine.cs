using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionEffect;  // ���� ����Ʈ ������
    public AudioClip explosionSound;    // ���� ����
    public float explosionRadius = 5f;  // ���� ����

    private bool hasExploded = false;   // ���� ���� Ȯ��

    public AudioSource warningSound; // ����� ����� �ҽ� �߰�
    public float warningInterval = 20f; // ����� ��� ����
    public float intervalVariation = 5f; // ����� ��� ������ ������ ���� ����

    private void Start()
    {
        // ����� ����� ���� �ڷ�ƾ ����
        StartCoroutine(PlayWarningSound());
    }
    void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && other.CompareTag("Player")) // 'Player' �±׸� ���� ��ü�� ����
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // ���� ���� ���
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // ���� ����Ʈ ����
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // ���� ���� ���� ��� Collider �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // 'Player' �±׸� ���� ��ü�� ���� ��� ó��
            if (nearbyObject.CompareTag("Player"))
            {
                Invoke("HPDown", 1);
                
            }
        }
    }

    void HPDown()
    {
        Player.instance.currentHP -= 100;
        Destroy(gameObject); //���� �ı�
    }

    void OnDrawGizmosSelected()
    {
        // �����Ϳ��� ���� ������ �ð�ȭ
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
