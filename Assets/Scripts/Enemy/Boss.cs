using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public BossAction[] actions;

    public int currentAction;
    private float actionCounter;

    private float shotCounter;
    public Vector2 moveDirection;
    public Rigidbody2D theRB;

    public int currentHealth;
    public int maxHealth;

    public GameObject deathEffect, hitEffect;
    public GameObject levelExit;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            moveDirection = Vector2.zero;

            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChase)
                {
                    Chase();
                }
                if (actions[currentAction].moveToPoints)
                {
                    MoveToPoints();
                }
            }

            if (actions[currentAction].shouldShoot)
            {
                Shoot();
            }

            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;
        }
        else
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;
        }
    }

    private void Chase()
    {
        moveDirection = PlayerController.instance.transform.position - transform.position;
        moveDirection.Normalize();
    }

    private void MoveToPoints()
    {
        moveDirection = actions[currentAction].positionToMove.position - transform.position;
        moveDirection.Normalize();

        if (Vector3.Distance(transform.position, actions[currentAction].positionToMove.position) < 0.1f)
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;
        }
    }

    private void Shoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = actions[currentAction].timeBetweenShots;

            foreach (Transform t in actions[currentAction].shotPoints)
            {
                Instantiate(actions[currentAction].bullet, t.position, t.rotation);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        Instantiate(hitEffect, transform.position, transform.rotation);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);
            levelExit.SetActive(true);

            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }
        }
    }

}


[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChase;
    public float moveSpeed;

    public bool moveToPoints;
    public Transform positionToMove;

    public bool shouldShoot;
    public GameObject bullet;
    public float timeBetweenShots;
    public Transform[] shotPoints;
}

