using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerTrap : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float delayBetweenAttacks = 1f;
    [SerializeField] float offsetTrap;
    [SerializeField] float offsetPlayer;
    bool playerWasTouched = false; 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && other.transform.position.y - offsetPlayer >= transform.position.y - offsetTrap)
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = true;
            StartCoroutine(DealDamage(otherGameObject));
        }
        else if (other.tag == "Enemy" && other.transform.position.y >= transform.position.y - offsetTrap)
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = true;
            StartCoroutine(DealDamage(otherGameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = false;
            StopCoroutine(DealDamage(otherGameObject));
        }
    }

    IEnumerator DealDamage(GameObject otherGameObject)
    {
        while(playerWasTouched && otherGameObject)
        {
            yield return new WaitForEndOfFrame();
            otherGameObject.GetComponent<Health>().TakeDamage(damage);
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    public float GetOffsetPlayer()
    {
        return offsetPlayer;
    }

    public float GetOffsetTrap()
    {
        return offsetTrap;
    }

}
