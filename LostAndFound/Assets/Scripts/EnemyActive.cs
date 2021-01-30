using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyActive : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    
    [Tooltip("At this distance from the Player the enemy will sleep again.")]
    [SerializeField] float distanceToAwake = 20f;
    Player player;
    bool enemyLighted = false;
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
        
            if(enemyLighted == false && !touchingPlayer && distanceToPlayer <= distanceToAwake)
            {
                Move();
            }
            if (distanceToPlayer >= distanceToAwake && enemyLighted == true)
            {
                SwitchEnemyState(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Flashlight>())
        {
            SwitchEnemyState(false);
        }
        if (other.GetComponent<Player>())
        {
            touchingPlayer = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<Flashlight>())
        {
            SwitchEnemyState(true);
        }
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
        enemyLighted = !newState;
        GetComponent<Light2D>().enabled = newState;
    }
}
