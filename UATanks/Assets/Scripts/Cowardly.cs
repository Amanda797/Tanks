using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowardly : AIController
{
    protected override void Update()
    {
        switch (aiState)
        {
            
            case States.Chase:
                Chase();
                if (canSeePlayer)
                {
                    ChangeState(States.Flee);
                }
                if (!canSeePlayer && !sense.isNearPlayer() && data.health.currentHealth > 75f)
                {
                    ChangeState(States.Patrol);
                }
                if (!canSeePlayer && !sense.isNearPlayer() && data.health.currentHealth < 75f)
                {
                    ChangeState(States.SearchPowerup);
                }
                break;
            case States.Flee:
                Flee();
                if(Vector3.Distance(transform.position, sense.player.position) > attackDistance)
                {
                    ChangeState(States.SearchTank);
                }
                break;
            case States.Patrol:
                Patrol();
                if(canSeePlayer)
                {
                    ChangeState(States.Flee);
                }
                break;
            case States.SearchTank:
                SearchTank();
                break;
            case States.SearchPowerup:
                SearchPowerup();
                if (!canSeePlayer && !sense.isNearPlayer() && data.health.currentHealth > 75f)
                {
                    ChangeState(States.Patrol);
                }
                if (canSeePlayer)
                {
                    ChangeState(States.Flee);
                }
                break;
            default:
                ChangeState(States.Idle);
                break;
        }
    }

   
    protected override void Chase()
    {
        data.mover.Move(transform.forward);
        data.mover.RotateTowards((target - transform.position).normalized);
    }
    protected override void SearchTank()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.enemies[0].transform.position);
        AIController closestTank = GameManager.instance.enemies[0];

        foreach(AIController enemy in GameManager.instance.enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                closestTank = enemy;
            }
        }

        target = closestTank.transform.position;

        ChangeState(States.Chase);
    }
}
