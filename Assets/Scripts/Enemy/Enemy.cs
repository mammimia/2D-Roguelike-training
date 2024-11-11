using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCore
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer body;
    public int health = 100;
    public GameObject hitEffect;
    public GameObject[] deathSplatter;
    [HideInInspector]
    public Dropable dropable;
}

[System.Serializable]
public class Shooting
{
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    [HideInInspector]
    public float fireCooldown;
    public float shootRange;
}

[System.Serializable]
public class Movement
{
    public bool shouldMove;
    public float moveSpeed;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
}

public class Enemy : MonoBehaviour
{
    public EnemyCore core;
    public Shooting shooting;
    public Movement movement;

    private void Awake()
    {
        core.dropable = GetComponent<Dropable>();
    }

    void Update()
    {
        if (core.body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (movement.shouldMove)
            {
                Move();

            }
            if (shooting.shouldShoot)
            {
                Shoot();
            }
        }
        else
        {
            core.rb.velocity = Vector3.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        core.health -= damage;
        Instantiate(core.hitEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(2);

        if (core.health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(1);
        int splatter = Random.Range(0, core.deathSplatter.Length);
        int rotation = Random.Range(0, 4);
        Instantiate(core.deathSplatter[splatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
        core.dropable?.handleItemDrop();
    }

    protected virtual void Shoot()
    {
        shooting.fireCooldown -= Time.deltaTime;

        if (shooting.fireCooldown <= 0)
        {
            shooting.fireCooldown = shooting.fireRate;
            Instantiate(shooting.bullet, shooting.firePoint.position, shooting.firePoint.rotation);
            AudioManager.instance.PlaySFX(13);
        }
    }

    protected virtual void Move()
    {
        movement.moveDirection.Normalize();

        core.rb.velocity = movement.moveDirection * movement.moveSpeed;

        if (movement.moveDirection != Vector3.zero)
        {
            core.anim.SetBool("isMoving", true);
        }
        else
        {
            core.anim.SetBool("isMoving", false);
        }
    }
}
