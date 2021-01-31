using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InterruptorDeLuz : MonoBehaviour
{
    [SerializeField] bool comienzaPrendido = false;
    [SerializeField] GameObject room;
    Light2D roomLight;
    Collider2D roomCollider;
    bool triggerCollision = false;

    void Start()
    {
        roomLight = room.GetComponent<Light2D>();
        roomCollider = room.GetComponent<Collider2D>();
        roomLight.enabled = comienzaPrendido;
        roomCollider.enabled = comienzaPrendido;
    }

    void Update()
    {
        LightsOn();
    }

    private void LightsOn()
    {
        if (triggerCollision)
        {
            if (Input.GetKeyDown("e"))
            {
                roomLight.enabled = !roomLight.enabled;
                roomCollider.enabled = !roomCollider.enabled;
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
