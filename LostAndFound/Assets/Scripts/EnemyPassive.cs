using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyPassive : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    
    [Tooltip("At this distance from the Player the enemy will sleep again.")]
    [SerializeField] float distanceToSleep = 1f;
    Player player;
    bool enemyLighted = false;
    bool enemyAwaked = false;
    bool touchingPlayer = false;

    void Start() 
    {
        GetComponent<Light2D>().enabled = false;
        player = FindObjectOfType<Player>();
    }

    void Update() 
    {
        if(player)
        {
            float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
        
            if(enemyAwaked && !touchingPlayer)
            {
                Move();
            }
            if (distanceToPlayer >= distanceToSleep && enemyLighted == false)
            {
                SwitchEnemyState(false);
            }
        }
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
