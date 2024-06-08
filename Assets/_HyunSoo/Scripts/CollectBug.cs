using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectBug : Enemy
{
    public bool isGivenItem = false; // change this due to motions and movements
    private Transform itembox;
    private bool isDamaging = false;
    public AudioSource sound;

    public AudioClip[] bugClip;
    public override void Start()
    {
        base.Start();
        name = "CollectBug";
        itembox = GameObject.Find("itembox").GetComponent<Transform>();
        hp = 1.0f;
        score = 1;
        sound = GetComponent<AudioSource>();
        sound.loop = true;
        sound.Play();
    }

    public override void Update()
    {
        base.Update();
        if (state == State.wander)
        {
            ChangeSound(bugClip[0], sound);
        }
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
            ChangeSound(bugClip[1], sound);
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
            {
                if (!isDamaging)
                {
                    StartCoroutine(Damage(1.0f)); // Start damaging with a delay of 3 seconds
                }
            }
        }
        if (state == State.pickup)
            agent.SetDestination(itembox.position);
        if (isGivenItem == true)
            state = State.pickup;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            sound.volume = 1.0f - (distance / detectionRange);
        }
        else
        {
            sound.volume = 0;
        }
    }
    public void ChangeSound(AudioClip clip, AudioSource source)
    {
        if (source.clip != clip)
        {
            source.clip = clip;
            source.loop = !source.loop;
            source.Play();
        }
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
