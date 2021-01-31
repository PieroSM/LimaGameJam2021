using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    Transform target;
    void Start()
    {
        target = transform.GetChild(0);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Enemy") && this.enabled)
        {
            collision.transform.position = target.position;
        }
    }

}
