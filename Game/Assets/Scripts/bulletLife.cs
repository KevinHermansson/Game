using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifespan = 1f; // Lifespan of the bullet in seconds

    void Start()
    {
        // Destroy the bullet after the specified lifespan
        Destroy(gameObject, lifespan);
    }
}
