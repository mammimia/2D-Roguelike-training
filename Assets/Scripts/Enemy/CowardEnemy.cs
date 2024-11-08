using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardEnemy : Enemy
{
    public float runawayRange;

    protected override void Move()
    {
        movement.moveDirection = Vector3.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange)
        {
            movement.moveDirection = transform.position - PlayerController.instance.transform.position;
        }

        base.Move();
    }
}