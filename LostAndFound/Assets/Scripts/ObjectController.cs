using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] float minDistanceToGrab = 0.5f;

    bool triggerCollision = false;
    CircleCollider2D objectCollider;
    [SerializeField] ItemDisplay itemDisplay;

    int borrar=0;
    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<CircleCollider2D>();
        objectCollider.radius = minDistanceToGrab;
    }

    void Update()
    {
        GrabItem();
    }

    private void GrabItem()
    {
        if (triggerCollision)
        {
            if (Input.GetKeyDown("e"))
            {
                itemDisplay.AddItemsToInventory();
                Destroy(gameObject);
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

    public ItemDisplay GetItemDisplay()
    {
        return itemDisplay;
    }

}
