using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float fireRate = 1f; // Rate of fire in bullets per second
    public GameObject projectilePrefab; // Prefab of the projectile object
    public Transform firePoint; // Point where the projectiles will be spawned from
    public float projectileSpeed = 10f; // Speed of the projectile

    private float nextFireTime; // Time when the next projectile can be fired

    void Start()
    {
        // Set player reference to the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Face the player directly without any interpolation
        transform.LookAt(player);

        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            // Fire a projectile
            FireProjectile();

            // Update next fire time
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireProjectile()
    {
        // Instantiate projectile prefab at the fire point
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

        // Check if the Rigidbody component is not null and set the velocity
        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = firePoint.forward * projectileSpeed;
        }
    }
}
