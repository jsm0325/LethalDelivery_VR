using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRadius = 10.0f;
    private bool isPatrolling;
    private bool lostPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;
    private float waitTime;
    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        isPatrolling = true;
    }

    protected virtual void Update()
    {
        // �÷��̾� ���� �Ÿ� Ȯ��
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < detectionRadius)
        {
            isPatrolling = false;
            agent.speed = 5.0f;
            ChasePlayer();
        }
        else
        {
            isPatrolling = true;
        }

        if (isPatrolling)
        {
            agent.speed = 3.5f;
            Patrol();
        }
    }

    protected virtual void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walk point�� �����ߴ��� Ȯ��
        if (distanceToWalkPoint.magnitude < 1f)
        {
            if (waitTime <= 0)
            {
                walkPointSet = false;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    protected virtual void SearchWalkPoint()
    {
        // ������ ��ġ ���
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
            waitTime = Random.Range(minWaitTime, maxWaitTime); // ���� ��� �ð� ����
        }
    }

    protected virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
