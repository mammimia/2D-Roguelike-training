using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{

    public float rangeToChase;

    protected override void Move()
    {
        movement.moveDirection = Vector3.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
        {
            movement.moveDirection = PlayerController.instance.transform.position - transform.position;
        }

        base.Move();
    }
}