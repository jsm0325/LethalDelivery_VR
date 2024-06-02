using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEye : Enemy
{
    private AudioSource audioSource;
    private float detectionRange;
    void Awake()
    {
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        name = "NoEye";
        detectionRange = 5.0f;
    }
    public override void Update()
    {
        base.Update();
        if (audioSource.isPlaying && state == State.kill)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == "Player")
                {
                    state = State.kill;
                    agent.SetDestination(player.position);
                    if (Vector3.Distance(transform.position, player.position) <= killDis)
                        GameObject.Destroy(player.gameObject);
                    break;
                }
            }
        }
    }
}
