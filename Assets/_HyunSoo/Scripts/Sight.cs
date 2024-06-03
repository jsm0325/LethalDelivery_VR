using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (transform.GetComponentInParent<Enemy>().name == "Giant" || transform.GetComponentInParent<Enemy>().name == "CollectBug")
                transform.GetComponentInParent<Enemy>().state = Enemy.State.encounter;
            else
            {
                transform.GetComponentInParent<Enemy>().state = Enemy.State.encounter;
            }
        }
        if (col.tag == "item" && transform.GetComponentInParent<Enemy>().name == "CollectBug")
        {
            //collect
            transform.GetComponentInParent<CollectBug>().isGivenItem = true;
            transform.GetComponentInParent<Enemy>().anim.SetTrigger("Pickup");
        }
        if (col.tag == "itembox" && transform.GetComponentInParent<Enemy>().name == "CollectBug")
        {
            //collect
            transform.GetComponentInParent<CollectBug>().isGivenItem = false;
            transform.GetComponentInParent<CollectBug>().state = Enemy.State.wander;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            if (transform.GetComponentInParent<Enemy>().name == "Giant" && transform.GetComponentInParent<Giant>().isPlayerBig == false)
            {
                transform.GetComponentInParent<Enemy>().state = Enemy.State.kill;
            }
            else if(transform.GetComponentInParent<Enemy>().name == "Giant" && transform.GetComponentInParent<Giant>().isPlayerBig == true)
            {
                transform.GetComponentInParent<Enemy>().state = Enemy.State.wander;
            }

            if (transform.GetComponentInParent<Enemy>().name == "NoEye")
            {
                transform.GetComponentInParent<Enemy>().state = Enemy.State.kill;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (transform.GetComponentInParent<Enemy>().name == "Giant" || transform.GetComponentInParent<Enemy>().name == "CollectBug")
                transform.GetComponentInParent<Enemy>().state = Enemy.State.wander;
            else
            {
                transform.GetComponentInParent<Enemy>().state = Enemy.State.wander;
            }
        }
    }
}
