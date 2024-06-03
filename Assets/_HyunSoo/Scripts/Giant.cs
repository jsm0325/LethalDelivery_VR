using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    public bool isPlayerBig = false; // change this due to motions and movements

    private Transform playerLeftArmPos;
    private Transform playerRightArmPos;
    void Awake()
    {
        name = "Giant";
        //playerLeftArmPos = GameObject.FindGameObjectWithTag("PlayerLeftArm").GetComponent<Transform>();
        //playerRightArmPos = GameObject.FindGameObjectWithTag("PlayerRightArm").GetComponent<Transform>();
    }
    public override void Update()
    {
        base.Update();
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
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
            {
                anim.SetTrigger("Attack");
                GameObject.Destroy(player.gameObject);
            }
        }
        if (Vector3.Distance(playerLeftArmPos.position, playerRightArmPos.position) > 1.0f)
            isPlayerBig = true;
        else
            isPlayerBig = false;
    }
}
