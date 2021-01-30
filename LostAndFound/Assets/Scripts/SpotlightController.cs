using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{


    void Start()
    {

    }

    void Update()
    {
        //Transform from Screen coordinates (pixels) to World coordinates (units)
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        transform.position = worldPos;

    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Enemy")
        {
            Debug.Log("Spotted!");
            Vector3 posDiff = otherCollider.transform.position - transform.position;
            otherCollider.GetComponent<Enemy>().RunAway(posDiff.normalized);
        }


    }

}
