using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSense : MonoBehaviour
{
    public float sightRadius;
    public float sightAngle;
    public Vector3 lastSightingV3;
    public LayerMask obstacleMask;
    public LayerMask playerMask;

    [HideInInspector] public GameObject lastSighting;
    [HideInInspector] public Transform player;
    AIController controller;

    Collider[] players;



    private void Start()
    {
        controller = GetComponent<AIController>();
    }

    private void Update()
    {
        players = Physics.OverlapSphere(transform.position, sightRadius, playerMask);    

        foreach(Collider player in players)
        {
            if(player.gameObject.CompareTag("PlayerOne") || player.gameObject.CompareTag("PlayerTwo"))
            {
                if(lastSighting == null)
                {
                    lastSighting = new GameObject();
                }

                Vector3 directionToTarget = controller.target - transform.position;

                if(Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
                {
                    this.player = player.transform;
                    lastSighting.transform.position = this.player.position;
                    controller.canSeePlayer = true;
                    
                }
                else
                {
                    controller.canSeePlayer = false;
                }
            }
            else
            {
                controller.canSeePlayer = false;
            }
        }

    }

    public bool isNearPlayer()
    {
        Debug.Log(players != null);

        if(players == null || players.Length == 0)
        {
            return false;
        }

        return true;
    }

    public bool canSeeObstacle()
    {
        RaycastHit hit;

        if(Physics.Raycast(gameObject.transform.position, transform.forward, out hit,sightRadius, obstacleMask))
        {
            Debug.DrawRay(gameObject.transform.position, transform.forward * hit.distance, Color.yellow);
            return true;
        }

        return false;
    }

}
