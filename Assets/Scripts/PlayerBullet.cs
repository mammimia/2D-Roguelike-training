using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;

    public Rigidbody2D rb;

    public GameObject impactEffect;

    public int damage = 50;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().takeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
