using UnityEngine;

public class TankMover : MonoBehaviour
{
    // Give move speed and rotate speed to set in inspecter.
    [Header("Movement")]
    public float moveSpeed;
    public float rotateSpeed;

    // Make a bullet prefab and a way to spawn in the bullets on every tank.
    [Header("Shooting")]
    // Getting Game Oject bullet prefab and making a bullet spawn.
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    TankData data;
    CharacterController cc;
    float shootDelay;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        data = GetComponent<TankData>();
    }

    private void Update()
    {
        // Gives all bullets a delay to shoot so they don't shoot constantly
        if(shootDelay > 0)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    public void Move(Vector3 directionToMove)
    {
        cc.SimpleMove(directionToMove * moveSpeed * Time.deltaTime);
    }

    public void Rotate(float direction)
    {
        transform.Rotate(0f, direction * rotateSpeed * Time.deltaTime, 0f);
    }

    public void Rotate()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    public void RotateTowards(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0f;
        lookRotation.z = 0f;

        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;

    }

    public void Shoot()
    {
        if(shootDelay <= 0)
        {
            // To wait time between shooting so don't contstantly shoot
            // Instantiating bullet
            // Whoever shot it, the tank data gets put on that tank.
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Bullet>().bulletOwner = data;
            shootDelay = data.fireRate;
        }
    }
}
