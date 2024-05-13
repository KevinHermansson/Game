using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public int damage = 10;
    public float knockbackForce = 50f;
    public float knockbackDuration = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isKnockedBack && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime));

            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                

                Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                

                StartCoroutine(HandleKnockback());
            }
        }
    }

    private IEnumerator HandleKnockback()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}