using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEye : Enemy
{
    private AudioSource audioSource;
    private float detectionRange;
    private bool isDamaging = false;
    public bool isPlayerStanding = true;
    private Transform playerHeadPos;
    private Transform rightLegPos;
    public override void Start()
    {
        base.Start();
        //audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        name = "NoEye";
        detectionRange = 5.0f;
        hp = 3.0f;
<<<<<<< Updated upstream
        playerHeadPos = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<Transform>();
        rightLegPos = GameObject.FindGameObjectWithTag("PlayerRightLeg").GetComponent<Transform>();
=======
        score = 5;
>>>>>>> Stashed changes
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
            if (audioSource.isPlaying || isPlayerStanding == true)
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
                            if (!isDamaging)
                            {
                                StartCoroutine(Damage(2.0f)); // Start damaging with a delay of 3 seconds
                            }
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
        if (Vector3.Distance(playerHeadPos.position, rightLegPos.position) > 1.0f)
            isPlayerStanding = true;
        else
            isPlayerStanding = false;
    }
    IEnumerator Damage(float delay)
    {
        isDamaging = true; // Set flag to true to prevent multiple coroutines
        yield return new WaitForSeconds(delay);
        transform.LookAt(player.transform);
        anim.SetTrigger("Attack");
        player.GetComponent<Player>().currentHP -= 5;
        yield return new WaitForSeconds(delay); // Add delay between attacks
        isDamaging = false; // Reset flag after delay
    }
}
