using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float decelaration = 5f;
    private Vector3 moveDirection;

    public float lifeTime = 3f;
    public SpriteRenderer sr;
    public float fadeSpeed = 2.5f;

    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, decelaration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.MoveTowards(sr.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (sr.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
