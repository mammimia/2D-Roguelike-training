using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public float rangeToChase;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 100;
    public GameObject[] deathSplatter;
    public GameObject hitEffect;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCooldown;
    public float shootRange;

    public SpriteRenderer body;

    void Start()
    {

    }

    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            handleMovement();
            if (shouldShoot && Vector3.Distance(PlayerController.instance.transform.position, transform.position) < shootRange)
            {
                handleShooting();
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void handleMovement()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        Instantiate(hitEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(2);

        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);
            int splatter = Random.Range(0, deathSplatter.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatter[splatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
        }
    }

    void handleShooting()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            fireCooldown = fireRate;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            AudioManager.instance.PlaySFX(13);
        }
    }
}
