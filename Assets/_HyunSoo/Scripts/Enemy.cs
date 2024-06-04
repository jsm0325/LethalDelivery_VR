using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public string name;

    public float hp = 10;

    private float wanderRadius = 30.0f;
    public enum State { wander, encounter, kill, pickup };
    public State state;
    public float killDis = 0.5f;
    private Vector3 wanderPosition;
    public Animator anim;

    public virtual void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
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

        if (hp == 0)
        {
            agent = null;
            StartCoroutine(Remove(5.0f));
        }
    }

    IEnumerator Remove(float delay)
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
