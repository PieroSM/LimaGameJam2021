using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool triggerCollision = false;
    [SerializeField] ItemDisplay itemRequired;
    [SerializeField] int numberOfItemsToOpen;
    [SerializeField] GameObject Pivot;
    [SerializeField] Animator doorAnimator;

    void Start()
    {
        doorAnimator.enabled = false;
    }

    
    void Update()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (triggerCollision)
        {
            if (Input.GetKeyDown("e") && itemRequired.GetNumberOfItems() >= numberOfItemsToOpen)
            {
                // Debug.Log("Abrir puerta");
                itemRequired.TakeItemsFromInventory(numberOfItemsToOpen);
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(AnimateDoor());
            }
            else if (Input.GetKeyDown("e"))
            {
                // Debug.Log("Faltan llaves");
            }
        }
    }

    IEnumerator AnimateDoor()
    {
        if (transform.parent.localScale.x < 0)
        {
            transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);    
            while (Mathf.Abs(transform.parent.rotation.eulerAngles.z) >= 270f)
            {
                transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);
                yield return null;
            }
        }
        else if (transform.parent.localScale.x > 0)
        {
            transform.parent.Rotate(Vector3.forward, 90f * Time.deltaTime);
            while (Mathf.Abs(transform.parent.rotation.eulerAngles.z) < 90f)
            {
                transform.parent.Rotate(Vector3.forward, 90f * Time.deltaTime);
                yield return null;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>())
        {
            triggerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Player>())
        {
            triggerCollision = false;
        }
    }

}
