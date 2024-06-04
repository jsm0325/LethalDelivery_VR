using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectBug : Enemy
{
    public bool isGivenItem = false; // change this due to motions and movements
    private Transform itembox;
    private bool isDamaging = false;
    public override void Start()
    {
        base.Start();
        name = "CollectBug";
        //itembox = GameObject.Find("itembox").GetComponent<Transform>();
        hp = 1.0f;
    }

    public override void Update()
    {
        base.Update();
        if (state == State.encounter)
        {
            //anim encounter, check isGivenItem
            if (isGivenItem == false)
                state = State.kill;
            else
                state = State.wander;
        }
        if (state == State.kill)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
            {
                if (!isDamaging)
                {
                    StartCoroutine(Damage(1.0f)); // Start damaging with a delay of 3 seconds
                }
            }
        }
        if (isGivenItem == true)
            state = State.pickup;
        if (state == State.pickup)
            agent.SetDestination(itembox.position);
    }
    IEnumerator Damage(float delay)
    {
        isDamaging = true; // Set flag to true to prevent multiple coroutines
        yield return new WaitForSeconds(delay);
        transform.LookAt(player.transform);
        anim.SetTrigger("Attack");
        player.GetComponent<Player>().currentHP -= 3;
        yield return new WaitForSeconds(delay); // Add delay between attacks
        isDamaging = false; // Reset flag after delay
    }
}
