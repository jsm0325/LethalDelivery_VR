using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionEffect;  // ���� ����Ʈ ������
    public AudioClip explosionSound;    // ���� ����
    public float explosionRadius = 5f;  // ���� ����

    private bool hasExploded = false;   // ���� ���� Ȯ��

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
        //AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // ���� ����Ʈ ����
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // ���� ���� ���� ��� Collider �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // 'Player' �±׸� ���� ��ü�� ���� ��� ó��
            if (nearbyObject.CompareTag("Player"))
            {
                // ���⼭�� ������ �α׸� ������, ���� ���ӿ����� �÷��̾��� ��� ó�� ������ ����
                Debug.Log("Player killed by mine");
            }
        }

        // ���� ��ü �ı�
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // �����Ϳ��� ���� ������ �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
