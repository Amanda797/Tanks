using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular : AIController
{
    protected override void Update()
    {
        switch (aiState)
        {
            case States.Attack:
                Attack();
                if (Vector3.Distance(transform.position, target) < attackDistance && canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if (!canSeePlayer)
                {
                    ChangeState(States.SearchTank);
                }
                if (sense.isNearPlayer() && data.health.currentHealth < 25f)
                {
                    ChangeState(States.Flee);
                }
                break;
            case States.Chase:
                Chase();
                if (Vector3.Distance(transform.position, target) <= attackDistance)
                {
                    ChangeState(States.Attack);
                }
                if (data.health.currentHealth <= 25f)
                {
                    ChangeState(States.Flee);
                }
                if (!canSeePlayer)
                {
                    ChangeState(States.SearchTank);
                }
                break;
            case States.Flee:
                Flee();
                if (!sense.isNearPlayer() && data.health.currentHealth > 25f)
                {
                    ChangeState(States.Patrol);
                }
                break;
            case States.Patrol:
                Patrol();
                if (canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if (sense.isNearPlayer() && data.health.currentHealth < 25f)
                {
                    ChangeState(States.Flee);
                }
                break;
            case States.SearchTank:
                SearchTank();
                if (canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                if (!canSeePlayer)
                {
                    ChangeState(States.Patrol);
                }
                break;
            default:
                ChangeState(States.Idle);
                break;
        }

        base.Update();
    }

}
