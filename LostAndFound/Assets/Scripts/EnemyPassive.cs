using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyPassive : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    
    [Tooltip("At this distance from the Player the enemy will sleep again.")]
    [SerializeField] float distanceToSleep = 10f;
    Player player;
    bool enemyLighted = false;
    bool enemyAwaked = false;
    bool touchingPlayer = false;

    [SerializeField] AudioClip awakeSound;
    [SerializeField] [Range(0, 1)] float volumeAwake = 1f;
    [SerializeField] AudioClip sleepSoundClose;
    [SerializeField] [Range(0, 1)] float volumeClose = 1f;
    [SerializeField] AudioClip sleepSoundMedium;
    [SerializeField] [Range(0, 1)] float volumeMedium = 0.8f;
    [SerializeField] AudioClip sleepSoundFar;
    [SerializeField] [Range(0, 1)] float volumeFar = 0.8f;

    [SerializeField] float distanceFar = 100f;
    [SerializeField] float distanceMedium = 50f;
    [SerializeField] float distanceClose = 20f;
    AudioSource audioSource;
    bool finishedAudio = true;


    void Start() 
    {
        GetComponent<Light2D>().enabled = false;
        player = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if(player)
        {
            float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
        
            if(enemyAwaked && !touchingPlayer)
            {
                //AudioSource.PlayClipAtPoint(awakeSound, Camera.main.transform.position);
                //audioSource.PlayOneShot(awakeSound);
                PlayAudioWithWait(awakeSound, volumeAwake);
                Move();
            }
            if (distanceToPlayer >= distanceToSleep && enemyLighted == false)
            {
                
                SwitchEnemyState(false);
            }
            if (!enemyAwaked)
            {
                //Debug.Log(distanceToPlayer);
                if (distanceToPlayer <= distanceClose)
                {
                    PlayAudioWithWait(sleepSoundClose, volumeClose);
                }
                else if (distanceToPlayer <= distanceMedium)
                {
                    PlayAudioWithWait(sleepSoundMedium, volumeMedium);
                }
                else if (distanceToPlayer <= distanceFar)
                {
                    PlayAudioWithWait(sleepSoundFar, volumeFar);
                }
            }
        }
    }
    void PlayAudioWithWait(AudioClip clip, float volume)
    {
        if (finishedAudio)
        {
            finishedAudio = false;
            StartCoroutine(ProcessAudio(clip, volume));
        }
    }

    IEnumerator ProcessAudio(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
        yield return new WaitForSeconds(clip.length);
        finishedAudio = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Enemigo iluminado");
        enemyLighted = true;
        SwitchEnemyState(true);
        if (other.GetComponent<Player>())
        {
            touchingPlayer = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) 
    {
        // Debug.Log("Enemigo no iluminado");
        enemyLighted = false;
        if (other.GetComponent<Player>())
        {
            touchingPlayer = false;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
    
    private void SwitchEnemyState(bool newState)
    {
        enemyAwaked = newState;
        GetComponent<Light2D>().enabled = newState;
    }
}
