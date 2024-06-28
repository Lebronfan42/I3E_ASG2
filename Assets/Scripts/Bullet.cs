using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f; // Damage the bullet inflicts
    private Vector3 direction;

    // Set the direction of the bullet
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // Move the bullet in a straight line in the set direction
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        Player playerHealth = other.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Apply damage to the player
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}




