using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum States
    { 
        Idle, Chase, Attack, Flee, SearchTank, SearchPowerup, Shout, Patrol
    }

    public enum AvoidenceState
    {
        None, TurnToAvoid, MoveToAvoid
    }

    public enum PatrolType
    {
        Loop
    }

    public TankData data;
    public TankSense sense;

    public States aiState;
    public AvoidenceState avoidenceStates;
    public PatrolType partolType;

    
    public float closeEnough;
    public float attackDistance;

    public float moveToAvoidTime;
    float moveTimer;

    public bool canSeePlayer = false;

    public Transform[] waypoints;
    protected int currentWaypoint = 0;

    public Vector3 target;


    protected void Start()
    {
        GameManager.instance.enemies.Add(this);
        target = waypoints[currentWaypoint].position;
    }

    protected void ChangeState(States newState)
    {
        aiState = newState;
    }

    protected void ChangeState(AvoidenceState newState)
    {
        avoidenceStates = newState;
    }

    protected virtual void Update()
    {
        AvoidObstacles();
    }

    protected void AvoidObstacles()
    {
        switch (avoidenceStates)
        {
            case AvoidenceState.None:
                AvoidNone();

                if(sense.canSeeObstacle())
                {
                    ChangeState(AvoidenceState.TurnToAvoid);
                }
                break;
            case AvoidenceState.TurnToAvoid:
                AvoidTurn();

                if (!sense.canSeeObstacle())
                {
                    moveTimer = moveToAvoidTime;
                    ChangeState(AvoidenceState.MoveToAvoid);
                }

                break;
            case AvoidenceState.MoveToAvoid:
                AvoidMove();

                if(sense.canSeeObstacle())
                {
                    ChangeState(AvoidenceState.TurnToAvoid);
                }

                break;
        }

    }

    protected void AvoidNone()
    {
        data.mover.Move(transform.forward);
        data.mover.RotateTowards((target - transform.position).normalized);
    }

    protected void AvoidTurn()
    {
        data.mover.Rotate();
    }

    protected void AvoidMove()
    {
        data.mover.Move(transform.forward);
        moveTimer -= Time.deltaTime;

        if(moveTimer <= 0)
        {
            ChangeState(AvoidenceState.None);
        }
    }

    protected virtual void Attack()
    {
        data.mover.Shoot();
    }
    protected virtual void Chase()
    {
        target = sense.player.position;
    }
    protected virtual void Flee()
    {
        target = (target - transform.position) * -1f;
    }
    protected virtual void Idle()
    {
        //default state should something go wrong
    }
    protected virtual void Patrol()
    {
        if(Vector3.Distance(transform.position, target) < closeEnough)
        {
            if (partolType == PatrolType.Loop)
            {
                if (currentWaypoint >= waypoints.Length - 1)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    currentWaypoint++;
                } 
            }

            //put other partol types here
        }

        target = waypoints[currentWaypoint].position;
    }
    protected virtual void SearchTank()
    {
        target = sense.lastSighting.transform.position;
    }

    protected virtual void SearchPowerup()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.powerups[0].transform.position);
        Transform closestPowerup = GameManager.instance.powerups[0].transform;

        foreach (PowerUp powerup in GameManager.instance.powerups)
        {
            if (Vector3.Distance(transform.position, powerup.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, powerup.transform.position);
                closestPowerup = powerup.transform;
            }
        }

        target = closestPowerup.position;
    }
    protected virtual void Shout()
    {

    }

    public void Alert(Vector3 position)
    {
        target = position;
        data.mover.Move(transform.forward);
        data.mover.RotateTowards((target - transform.position).normalized);
    }


}
