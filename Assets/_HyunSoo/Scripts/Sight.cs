using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if (transform.GetComponentInParent<Enemy>().name == "Giant" || transform.GetComponentInParent<Enemy>().name == "CollectBug")
                transform.GetComponentInParent<Enemy>().state = Enemy.State.kill;
            else
            {
                transform.GetComponentInParent<NoEye>().isEarOn = true;
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
                transform.GetComponentInParent<NoEye>().isEarOn = false;
                transform.GetComponentInParent<Enemy>().state = Enemy.State.wander;
            }
        }
    }
}
