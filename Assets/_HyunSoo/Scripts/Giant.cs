using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    public bool isPlayerBig = false; // change this due to motions and movements

    private Transform playerLeftArmPos;
    private Transform playerRightArmPos;
    private bool isDamaging = false;
    public override void Start()
    {
        base.Start();
        name = "Giant";
        playerLeftArmPos = GameObject.FindGameObjectWithTag("PlayerLeftArm").GetComponent<Transform>();
        playerRightArmPos = GameObject.FindGameObjectWithTag("PlayerRightArm").GetComponent<Transform>();
        hp = 10.0f;
        score = 20;
    }
    public override void Update()
    {
        base.Update();
        if(state == State.wander)
        {
            anim.SetBool("Run", false);
        }
        if(state == State.encounter)
        {
            //anim encounter, check isplayerbig
            if (isPlayerBig == false)
                state = State.kill;
            else
                state = State.wander;
        }
        if(state == State.kill)
        {
            anim.SetBool("Run", true);
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
            {
                if (!isDamaging)
                {
                    StartCoroutine(Damage(3.0f)); // Start damaging with a delay of 3 seconds
                }
            }
        }
        if (Vector3.Distance(playerLeftArmPos.position, playerRightArmPos.position) > 1.0f)
            isPlayerBig = true;
        else
            isPlayerBig = false;
    }

    IEnumerator Damage(float delay)
    {
        isDamaging = true; // Set flag to true to prevent multiple coroutines
        yield return new WaitForSeconds(delay);
        transform.LookAt(player.transform);
        anim.SetTrigger("Attack");
        player.GetComponent<Player>().currentHP -= 10;
        yield return new WaitForSeconds(delay); // Add delay between attacks
        isDamaging = false; // Reset flag after delay
    }
}
