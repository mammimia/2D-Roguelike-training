using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private float activeMoveSpeed;
    public float dashSpeed = 8f;
    public float dashLength = .5f;
    public float dashCooldown = 1f;
    public float dashInvincibilityLength = .5f;
    private float dashActiveCounter, dashCooldownCounter;

    private Vector2 moveInput;
    Vector3 mousePosition;
    Vector3 screenPoint;

    public Rigidbody2D rb;
    public Transform gunArm;
    public Animator anim;


    private Camera mainCamera;

    public SpriteRenderer body;

    [HideInInspector]
    public bool canMove = true;

    public List<Gun> availableGuns = new List<Gun>();
    private int currentGunIndex;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            mousePosition = Input.mousePosition;
            screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

            movePlayer();
            animatePlayer();
            rotatePlayer();
            rotateGunArm();
            handleDash();
            handleGunSwitch();
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    void movePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;
    }

    void animatePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
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

    void handleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownCounter <= 0 && dashActiveCounter <= 0)
        {
            activeMoveSpeed = dashSpeed;
            dashActiveCounter = dashLength;
            anim.SetTrigger("dash");
            PlayerHealthController.instance.makeInvincible(dashInvincibilityLength);
            AudioManager.instance.PlaySFX(8);
        }

        if (dashActiveCounter > 0)
        {
            dashActiveCounter -= Time.deltaTime;

            if (dashActiveCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

    void handleGunSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (availableGuns.Count > 0)
            {
                currentGunIndex = (currentGunIndex + 1) % availableGuns.Count;
                switchGun();
            }
        }
    }

    public void switchGun()
    {
        foreach (Gun gun in availableGuns)
        {
            gun.gameObject.SetActive(false);
        }

        availableGuns[currentGunIndex].gameObject.SetActive(true);
    }

    public bool isDashing()
    {
        return dashActiveCounter > 0;
    }
}
