/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for the bullets sent out by the enemies in this game
 */


using UnityEngine;

/// <summary>
/// Represents a bullet in the game.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// The speed of the bullet.
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// The damage the bullet inflicts.
    /// </summary>
    public float damage = 10f;

    /// <summary>
    /// The direction the bullet will travel.
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// Sets the direction of the bullet.
    /// </summary>
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    /// <summary>
    /// Updates the bullet's position every frame.
    /// </summary>
    void Update()
    {
        // Move the bullet in a straight line in the set direction
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Called when the bullet collides with another collider.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hit a player
        Player playerHealth = other.GetComponent<Player>();
        if (playerHealth != null)
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damage);
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}





