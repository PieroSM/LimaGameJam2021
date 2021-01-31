using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] float waitToDestroy = 2f;
    [SerializeField] float secondsImmobile = 0.5f;
    HealthDisplay healthDisplay;
    List<GameObject> hearts;
    Animator animator;
    Player player;
    public bool isDead = false;

    void Start()
    {
        healthDisplay = FindObjectOfType<HealthDisplay>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    public void TakeDamage(int damage)
    {
        health-=damage;
        if (health > 0)
        {
            if (tag == "Player")
            {
                player.canMove = false;
                player.canMovePriority = false;
                healthDisplay.DisableHeart(health);
                animator.SetTrigger("IsHurt");
                StartCoroutine(ProcessHit());
            }
        }
        else
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        if (tag == "Player")
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetTrigger("IsDead");
            player.canMove = false;
            player.canMovePriority = false;
            StartCoroutine(ProcessDeath());
            Level level = FindObjectOfType<Level>();
            //Debug.Log(SceneManager.GetActiveScene().buildIndex);
            //level.SetLastLevelIndex(SceneManager.GetActiveScene().buildIndex);
            level.LoadGameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(waitToDestroy);
        Destroy(gameObject);
    }

    IEnumerator ProcessHit()
    {
        yield return new WaitForSeconds(secondsImmobile);
        player.canMove = true;
        if (!isDead)
        {
            player.canMovePriority = true;
        }
    }

    public int GetHealth()
    {
        return health;
    }

}
