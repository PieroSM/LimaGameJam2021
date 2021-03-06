﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float lightRadius = 1f;
    [SerializeField] float secondsUse = 0.3f;
    Animator animator;
    Flashlight flashlight;
    public bool canMove = true;
    public bool canMovePriority = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        flashlight = GetComponentInChildren<Flashlight>();
    }

    void Update()
    {
        if (canMove && canMovePriority)
        {
            Move();
        }
        PointFlashlight();
        SwitchFlashlight();
    }

    private void Move()
    {

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 deltaMove = inputDir.normalized * moveSpeed * Time.deltaTime;

        transform.Translate(deltaMove);
        if (inputDir.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (inputDir.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (inputDir.sqrMagnitude > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void PointFlashlight()
    {
        //Transform from Screen coordinates (pixels) to World coordinates (units)
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 relDir = ((Vector3)worldPos - transform.position).normalized * lightRadius;

        float angle = Mathf.Atan2(relDir.y, relDir.x) * Mathf.Rad2Deg;
        flashlight.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        if (transform.localScale.x < 0)
        {
            relDir.x = -relDir.x;
        }
        flashlight.transform.localPosition = relDir;
    }

    private void SwitchFlashlight()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flashlight.Switch();
            flashlight.GetComponent<Collider2D>().enabled = !flashlight.GetComponent<Collider2D>().enabled;
        }
    }

    public void AnimateUse()
    {
        canMove = false;
        animator.SetTrigger("IsUsing");
        StartCoroutine(ProcessUse());
    }

    IEnumerator ProcessUse()
    {
        yield return new WaitForSeconds(secondsUse);
        if (canMovePriority)
        {
            canMove = true;
        }
    }
}
