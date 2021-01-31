using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 10;
    HealthDisplay healthDisplay;
    List<GameObject> hearts;

    void Start()
    {
        healthDisplay = FindObjectOfType<HealthDisplay>();
    }

    public void TakeDamage(int damage)
    {
        if(tag == "Player")
        {
            healthDisplay.DisableHeart(health-1);
        }
        health-=damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {   
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

}
