using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart : AIController
{
    protected override void Update()
    {
        switch (aiState)
        {
            case States.Attack:
                Attack();
                if(Vector3.Distance(transform.position, target) < attackDistance && canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if(!canSeePlayer)
                {
                    ChangeState(States.SearchTank);
                }
                if(sense.isNearPlayer() && data.health.currentHealth < 25f)
                {
                    ChangeState(States.Flee);
                }
                break;
            case States.Chase:
                Chase();
                if(Vector3.Distance(transform.position, target) <= attackDistance)
                {
                    ChangeState(States.Shout);
                }
                if(data.health.currentHealth <= 25f)
                {
                    ChangeState(States.Flee);
                }
                if(!canSeePlayer)
                {
                    ChangeState(States.SearchTank);
                }
                break;
            case States.Flee:
                Flee();
                if(!sense.isNearPlayer() && data.health.currentHealth > 25f)
                {
                    ChangeState(States.Patrol);
                }
                if(!sense.isNearPlayer() && data.health.currentHealth < 25f)
                {
                    ChangeState(States.SearchPowerup);
                }
                break;
            case States.Patrol:
                Patrol();
                if(canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if(sense.isNearPlayer() && data.health.currentHealth < 25f)
                {
                    ChangeState(States.Flee);
                }
                if(data.health.currentHealth < 25f)
                {
                    ChangeState(States.SearchPowerup);
                }
                break;
            case States.SearchTank:
                SearchTank();
                if(canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if(!canSeePlayer)
                {
                    ChangeState(States.Patrol);
                }
                break;
            case States.Shout:
                Shout();
                break;
            default:
                ChangeState(States.Idle);
                break;
        }

        base.Update();
    }

    protected override void Chase()
    {
        target = sense.player.GetComponentInChildren<SmartFollow>().transform.position;
    }

    protected override void Shout()
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

        
        closestTank.target = target;

        ChangeState(States.Attack);
    }
}
