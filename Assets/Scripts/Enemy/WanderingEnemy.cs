using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemy : Enemy
{

    public float rangeToChase, wanderTime, waitTime;
    private float wanderCounter, waitCounter;
    private Vector3 wanderDirection;

    private void Start()
    {
        waitCounter = Random.Range(waitTime * .75f, waitTime * 1.25f);
    }

    protected override void Move()
    {
        movement.moveDirection = Vector3.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
        {
            movement.moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            if (wanderCounter > 0)
            {
                wanderCounter -= Time.deltaTime;
                movement.moveDirection = wanderDirection;

                if (wanderCounter <= 0)
                {
                    waitCounter = Random.Range(waitTime * .75f, waitTime * 1.25f);
                }
            }

            if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;

                if (waitCounter <= 0)
                {
                    wanderCounter = Random.Range(wanderTime * .75f, wanderTime * 1.25f);

                    wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                }
            }
        }

        base.Move();
    }
}