using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // If don't add time for delay, game designers can set the delay for whatever time they want.
    public float destroyDelay;


    // Hide in Inspector so everyone can't see it in inspector, but this gives every bullet a tank owner so that can set sccore later
    [HideInInspector] public TankData bulletOwner;

    // Give it speed, otherwise it can't go anywhere
    [SerializeField] float bulletSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed);

        // Bullet destroyed after a certain amount of time, like if bullet goes off map, it doesn't continue to form forever while playing.
        Destroy(this.gameObject, destroyDelay);
    }

    // OnTrigger Enter to see if bullet collides with something.
    private void OnTriggerEnter(Collider other)
    {
        // To see if bullet hit anything, so call health to take damage.
        // If anything has Health(tanks) it will get damaged.
        TankHealth otherHealth = other.GetComponent<TankHealth>();

        // If object doesn't have health, it will set equal to null, so nontanks will not take damage.
        // Bullet collided with tank 
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(10);
        }

        // If bullet hits something, no matter what bullet hits, bullet gets destroyed.
        Destroy(this.gameObject);
    }

}
