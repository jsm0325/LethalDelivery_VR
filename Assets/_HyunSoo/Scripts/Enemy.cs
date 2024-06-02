using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public string name;

    private float wanderRadius = 10.0f;
    public enum State { wander, encounter, kill, pickup };
    public State state;
    public float killDis = 0.5f;
    private Vector3 wanderPosition;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        state = State.wander;
    }
    public virtual void Update()
    {
        if (player == null)
        {
            return;
        }

        switch (state)
        {
            case State.wander:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    wanderPosition = Random.insideUnitSphere * wanderRadius + transform.position;
                    wanderPosition.y = transform.position.y;
                    agent.SetDestination(wanderPosition);
                }
                break;
        }
    }
}
