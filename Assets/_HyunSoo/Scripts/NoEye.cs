using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEye : Enemy
{
    private AudioSource audioSource;
    private float detectedRange;
    private bool isDamaging = false;
    public bool isPlayerStanding = true;
    private Transform playerHeadPos;
    private Transform rightLegPos;
    public AudioSource sound;

    public AudioClip[] noEyeClip;
    public override void Start()
    {
        base.Start();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        name = "NoEye";
        detectedRange = 5.0f;
        hp = 3.0f;
        score = 5;
        sound = GetComponent<AudioSource>();
        sound.loop = true;
        sound.Play();
        //playerHeadPos = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<Transform>();
        //rightLegPos = GameObject.FindGameObjectWithTag("PlayerRightLeg").GetComponent<Transform>();

    }
    public override void Update()
    {
        base.Update();
        /*if (Vector3.Distance(playerHeadPos.position, rightLegPos.position) > 1.0f)
            isPlayerStanding = true;
        else
            isPlayerStanding = false;*/
        if (state== State.wander)
        {
            ChangeSound(noEyeClip[0], sound);
            anim.SetBool("Run", false);
        }
        if (state == State.encounter)
        {
            if (isPlayerStanding == true)
            {
                ChangeSound(noEyeClip[1], sound);
                state = State.kill;
            }
            else
                state = State.wander;

            if (audioSource.isPlaying)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectedRange);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        state = State.kill;
                        break;
                    }
                }
            }
            /*else
            {
                state = State.wander;
            }*/
        }
        if(state == State.kill)
        {
            anim.SetBool("Run", true);
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) <= killDis)
            {
                if (!isDamaging)
                {
                    StartCoroutine(Damage(2.0f)); // Start damaging with a delay of 3 seconds
                }
            }
        }
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
        player.GetComponent<Player>().currentHP -= 5;
        yield return new WaitForSeconds(delay); // Add delay between attacks
        isDamaging = false; // Reset flag after delay
    }
}
