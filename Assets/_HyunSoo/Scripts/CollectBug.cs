using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectBug : Enemy
{
    public bool isGivenItem = false; // change this due to motions and movements

    void Awake()
    {
        name = "CollectBug";
    }

    public override void Update()
    {
        base.Update();
        if (state == State.kill && isGivenItem == false)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
                GameObject.Destroy(player.gameObject);
        }
    }
}
