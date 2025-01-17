using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector3 direction;

    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.takeDamage(damage);
        }

        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
