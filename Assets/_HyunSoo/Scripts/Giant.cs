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
        if(state == State.kill && isPlayerBig == false)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
                GameObject.Destroy(player.gameObject);
        }
    }
}
