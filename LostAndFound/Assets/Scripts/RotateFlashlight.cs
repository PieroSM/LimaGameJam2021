using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFlashlight : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
