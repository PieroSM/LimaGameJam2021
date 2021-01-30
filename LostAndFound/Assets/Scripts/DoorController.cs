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
                Debug.Log("Abrir puerta");
                itemRequired.TakeItemsFromInventory(numberOfItemsToOpen);
                //RotateDoor();
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;

                StartCoroutine(AnimateDoor());
            }
            else if (Input.GetKeyDown("e"))
            {
                Debug.Log("Faltan llaves");
            }
        }
    }

    IEnumerator AnimateDoor()
    {
        transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);
        Debug.Log(transform.parent.rotation.eulerAngles.z);
        while (Mathf.Abs(transform.parent.rotation.eulerAngles.z) < 90f)
        {
            Debug.Log(transform.parent.rotation.eulerAngles.z);

            if (transform.parent.localScale.x < 0)
            {
                transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);
            }
            else if (transform.parent.localScale.x > 0)
            {
                transform.parent.Rotate(Vector3.forward, 90f * Time.deltaTime);
            }
            Debug.Log(transform.parent.rotation.eulerAngles.z);
            yield return null;
        }
        
    }

    private void RotateDoor()
    {
        doorAnimator.enabled = true;
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
