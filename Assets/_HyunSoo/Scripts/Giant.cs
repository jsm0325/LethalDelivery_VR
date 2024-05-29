using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    public bool isPlayerBig = false; // change this due to motions and movements

    void Awake()
    {
        name = "Giant";
    }
    public override void Update()
    {
        base.Update();
        if(state == State.wander)
        {
            isPlayerBig = false;
        }
        if(state == State.encounter)
        {
            //anim encounter, check isplayerbig
        }
        if(state == State.kill)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
                GameObject.Destroy(player.gameObject);
        }
    }
}
