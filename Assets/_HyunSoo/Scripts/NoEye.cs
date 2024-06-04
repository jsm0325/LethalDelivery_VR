using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEye : Enemy
{
    private AudioSource audioSource;
    private float detectionRange;
    public override void Start()
    {
        base.Start();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        name = "NoEye";
        detectionRange = 5.0f;
        hp = 3.0f;
    }
    public override void Update()
    {
        base.Update();
        if (state== State.wander)
        {
            anim.SetBool("Run", false);
        }
        if (state == State.encounter)
        {
            if (audioSource.isPlaying)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        state = State.kill;
                        anim.SetBool("Run", true);
                        agent.SetDestination(player.position);
                        if (Vector3.Distance(transform.position, player.position) <= killDis)
                        {
                            print("Attack");
                            player.GetComponent<Player>().currentHP -= 10;
                            anim.SetTrigger("Attack");
                        }
                        break;
                    }
                }
            }
            else
            {
                state = State.wander;
            }
        }
        
    }
}
