using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public float speed;
    public int damage;
    private Vector3 direction;

    void Start()
    {
        direction = transform.right;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (!Boss.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
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
