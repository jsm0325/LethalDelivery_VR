using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectBug : Enemy
{
    public bool isGivenItem = false; // change this due to motions and movements
    private Transform itembox;

    void Awake()
    {
        name = "CollectBug";
        itembox = GameObject.Find("itembox").GetComponent<Transform>();
    }

    public override void Update()
    {
        base.Update();
        if (state == State.encounter)
        {
            //anim encounter, check isGivenItem
            if (isGivenItem == false)
                state = State.kill;
        }
        if (state == State.kill)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
                GameObject.Destroy(player.gameObject);
        }
        if (isGivenItem == true)
            state = State.pickup;
        if (state == State.pickup)
            agent.SetDestination(itembox.position);
    }
}
