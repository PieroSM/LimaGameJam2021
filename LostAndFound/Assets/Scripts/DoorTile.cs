using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTile : MonoBehaviour
{
    bool triggerCollision = false;
    [SerializeField] ItemDisplay itemRequired;
    [SerializeField] int numberOfItemsToOpen;
    [SerializeField] Tilemap map;
    [SerializeField] GameObject pivot;
    [SerializeField] TileBase doorOpenTile;
    Vector3Int pivotTransformInt;
    

    public void Start()
    {
        pivotTransformInt = WorldPosToInt(pivot.transform.position);
    }

    private Vector3Int WorldPosToInt(Vector3 worldPos)
    {
        return new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);
    }    void Update()
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
                ChangeTileBase();
            }
            else if (Input.GetKeyDown("e"))
            {
                // Debug.Log("Faltan llaves");
            }
        }
    }

    private void ChangeTileBase()
    {
        map.SetTile(pivotTransformInt, null);
        map.SetTile(pivotTransformInt, doorOpenTile);
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
