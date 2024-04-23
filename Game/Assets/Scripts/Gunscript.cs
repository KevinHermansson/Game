using UnityEngine;

public class HitscanGun : MonoBehaviour
{
    public float range = 100f;  // Maximum range of the gun
    public Transform gunTip;     // Transform representing the tip of the gun barrel
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public Color lineColor = Color.red; // Color of the line

    void Start()
    {
        // Ensure the LineRenderer is initialized
        InitializeLineRenderer();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // Assuming Fire1 is the left mouse button
        {
            Shoot();
        }
    }

    void InitializeLineRenderer()
    {
        // Create a new LineRenderer component if not already assigned
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set LineRenderer properties
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 raycastDirection = gunTip.forward * range;

        // Draw debug line to visualize the raycast direction
        Debug.DrawRay(gunTip.position, raycastDirection, Color.green, 0.1f);

        if (Physics.Raycast(gunTip.position, raycastDirection, out hit, range))
        {
            // Set LineRenderer positions to match raycast origin and hit point
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, hit.point);

            Debug.Log("Hit: " + hit.transform.name);

            // Check if the object hit has a health component
            Health targetHealth = hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                // Damage the target's health
                targetHealth.TakeDamage(10); // Change the damage value as needed
            }

            // Optionally, you can add visual effects for hitting the target
            // For example, spawning impact particles or applying force to the hit object
        }
        else
        {
            // Set LineRenderer positions to show full range of the raycast
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, gunTip.position + raycastDirection);

            Debug.Log("No hit detected.");
        }
    }
}
