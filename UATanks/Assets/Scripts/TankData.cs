using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // Making a mover to move tanks in Tankdata.
    [Header("Components")]
    public TankMover mover;

    [Header("Stats")]
    // number of seconds before you can fire again.
    public float fireRate;
    // Making tankhealth available in tank data.
    public TankHealth health;
}
