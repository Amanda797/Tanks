using UnityEngine;

public class TankHealth : MonoBehaviour
{
    // made a current health variable
    public float currentHealth;
    // made a Maximum health variable AI and player tanks can have a maximum amount of health
    public float maxHealth;


   
    private void Start()
    {
        // Adding maxHealth at the start of the game to all tanks, AI and player
        currentHealth = maxHealth;
    }

    // Makes it so tanks take damage if hit by bullet
    public void TakeDamage(float damage)
    {
        // Current Health of tank goes down if tank takes damage.
        currentHealth -= damage;
    }

}
