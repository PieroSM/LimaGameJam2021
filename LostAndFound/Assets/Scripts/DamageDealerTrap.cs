using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerTrap : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float delayBetweenAttacks = 1f;
    [SerializeField] float offsetTrap;
    [SerializeField] float offsetPlayer;
    [SerializeField] bool itsActiveEver = false;
    Collider2D trapCollider;
    SpriteRenderer trapSprite;
    Animator animator;
    bool playerWasTouched = false;
    int contador = 0;
    Coroutine dealDamage;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        trapCollider = GetComponent<Collider2D>();
        trapSprite = GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(!(itsActiveEver || contador < 1))
        {
            trapCollider.enabled = false;
            trapSprite.color = new Color32(255,0,0,255);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = true;
            dealDamage = StartCoroutine(DealDamage(otherGameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = false;
        }
    }
    IEnumerator DealDamage(GameObject colliderObject)
    {
        while(true)
        {
            if (colliderObject)
            {
                if (animator.GetBool("IsActivated"))
                {
                    yield return new WaitForEndOfFrame();
                    colliderObject.GetComponent<Health>().TakeDamage(damage);
                    contador ++;
                    yield return new WaitForSeconds(delayBetweenAttacks);
                }
            }  
            yield return null;
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
