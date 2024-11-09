using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatrolEnemy : Enemy
{

    public float rangeToChase;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    private void Start()
    {
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
            movement.moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
            {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            }
        }

        base.Move();
    }
}