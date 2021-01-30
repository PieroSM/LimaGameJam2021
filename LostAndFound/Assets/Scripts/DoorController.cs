using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool triggerCollision = false;
    [SerializeField] ItemDisplay itemRequired;
    [SerializeField] int numberOfItemsToOpen;

    
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
            }
            else if (Input.GetKeyDown("e"))
            {
                Debug.Log("Faltan llaves");
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
        triggerCollision = false;
    }

}
