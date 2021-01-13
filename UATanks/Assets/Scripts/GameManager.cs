using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make it a Singleton as per directions. Can only be one of them at all if its a singleton.
    // If it is in a region, it is hidden.
    #region Singleton

    // Access it anywhere if it static it will always be the same.
    public static GameManager instance;

    private void Awake()
    {
        // If the instance exists (not null) and it is not me
        if(instance != null && instance != this)
        {
            // Destroy it because it already exists and it is not needed.
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    // Make a list so players can add themselves to the list
    public List<TankData> players;
    // List of enemies, so enemies can add themselves
    public List<AIController> enemies;
    // List of pickups so pickups can add themselves.
    public List<PowerUp> powerups;
}
