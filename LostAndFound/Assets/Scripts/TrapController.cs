using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    Animator animator;
    DamageDealerTrap damageDealerTrap;
    float offsetPlayer = 1.35f;
    float offsetTrap = 0.6f;
    Coroutine animateTrap;
    void Start()
    {
        animator = GetComponent<Animator>();
        damageDealerTrap = GetComponent<DamageDealerTrap>();
        offsetPlayer = damageDealerTrap.GetOffsetPlayer();
        offsetTrap = damageDealerTrap.GetOffsetTrap();
    }

    IEnumerator AnimateTrap(GameObject colliderObject)
    {
        while(true)
        {
            if (colliderObject)
            {
                if (colliderObject.tag == "Player" && colliderObject.transform.position.y - offsetPlayer >= transform.position.y - offsetTrap)
                {
                    animator.SetBool("IsActivated", true);
                }
                else if (colliderObject.tag == "Enemy" && colliderObject.transform.position.y >= transform.position.y - offsetTrap)
                {
                    animator.SetBool("IsActivated", true);
                }
            }
            else 
            {
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                animator.SetBool("IsActivated", false);
            }
            yield return null;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject colliderObject = other.gameObject;
        if(colliderObject.tag == "Player" || colliderObject.tag == "Enemy")
        { 
            animateTrap = StartCoroutine(AnimateTrap(colliderObject)); 
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject colliderObject = other.gameObject;
        if(colliderObject.tag == "Player" || colliderObject.tag == "Enemy")
        {
            StopCoroutine(animateTrap); 
            Debug.Log(colliderObject.name);
            animator.SetBool("IsActivated", false);
        }
    }
}
