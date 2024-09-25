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

    void Start()
    {

    }

    void Update()
    {
        handleMovement();
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

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
