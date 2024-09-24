using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    Vector3 mousePosition;
    Vector3 screenPoint;

    public Rigidbody2D rb;
    public Transform gunArm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        movePlayer();
        rotatePlayer();
        rotateGunArm();
    }

    void movePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;
    }

    void rotatePlayer()
    {
        if (mousePosition.x < screenPoint.x && transform.localScale.x == 1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (mousePosition.x >= screenPoint.x && transform.localScale.x == -1f)
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }
    }

    void rotateGunArm()
    {
        Vector2 offset = mousePosition - screenPoint;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}
