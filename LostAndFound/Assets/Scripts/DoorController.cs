using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool triggerCollision = false;
    [SerializeField] ItemDisplay itemRequired;
    [SerializeField] int numberOfItemsToOpen;
    [SerializeField] Sprite openDoorSprite;
    [SerializeField] DoorController otherSideDoor;
    bool interactable = true;
    // [SerializeField] GameObject Pivot;
    // [SerializeField] Animator doorAnimator;

    void Start()
    {
        // doorAnimator.enabled = false;
    }


    void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (triggerCollision)
        {
            if (Input.GetKeyDown("e") && itemRequired.GetNumberOfItems() >= numberOfItemsToOpen)
            {
                HandleOpenningDoor();
                if (otherSideDoor != null)
                {
                    otherSideDoor.HandleOpenningDoor();
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                // Poner sonido de puerta que no se puede abrir aquí!
            }
        }
    }

    private void HandleOpenningDoor()
    {
        itemRequired.TakeItemsFromInventory(numberOfItemsToOpen);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<DoorTeleport>().enabled = true;
        interactable = false;
        ChangeToOpenDoorSprite();
    }

    private void ChangeToOpenDoorSprite()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() && interactable)
        {
            triggerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Player>() && interactable)
        {
            triggerCollision = false;
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

}
