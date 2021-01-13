using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressive : AIController
{
    protected override void Update()
    {
        switch (aiState)
        {
            case States.Attack:
                Attack();
                if(Vector3.Distance(transform.position, target) > attackDistance && canSeePlayer)
                {
                    ChangeState(States.Chase);
                }

                if(canSeePlayer == false)
                {
                    ChangeState(States.SearchTank);
                }
                break;
            case States.Chase:
                Chase();
                if (Vector3.Distance(transform.position, target) < attackDistance)
                {
                    ChangeState(States.Attack);
                }

                if (canSeePlayer == false)
                {
                    ChangeState(States.SearchTank);
                }
                break;
            case States.Patrol:
                Patrol();
                if (canSeePlayer)
                {
                    ChangeState(States.Chase);
                }
                break;
            case States.SearchTank:
                SearchTank();

                if (Vector3.Distance(transform.position, target) < closeEnough)
                {
                    if (canSeePlayer == true)
                    {
                        ChangeState(States.Chase);
                    }

                    if (canSeePlayer == false)
                    {
                        ChangeState(States.Patrol);
                    }
                }
                break;
            default:
                ChangeState(States.Idle);
                break;
        }

        base.Update();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }
    protected override void SearchTank()
    {
        target = sense.lastSighting.transform.position;
    }
}
