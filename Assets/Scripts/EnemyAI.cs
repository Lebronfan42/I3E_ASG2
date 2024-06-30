/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for the enemies and their behavior in the game 
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// Class responsible for the enemies and their behavior in the game.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// GameObject to spawn when the enemy dies.
    /// </summary>
    [SerializeField]
    private GameObject healthToSpawn;

    /// <summary>
    /// Reference to the player.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Detection range for the enemy.
    /// </summary>
    public float detectionRange = 20f;

    /// <summary>
    /// Range within which the enemy can shoot.
    /// </summary>
    public float shootingRange = 15f;

    /// <summary>
    /// Time between shots.
    /// </summary>
    public float fireRate = 1f;

    /// <summary>
    /// Bullet prefab to instantiate.
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Point from which bullets are fired.
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Speed of the bullet.
    /// </summary>
    public float bulletSpeed = 10f;

    /// <summary>
    /// Time until the next shot can be fired.
    /// </summary>
    private float nextTimeToFire = 0f;

    /// <summary>
    /// Enemy's health.
    /// </summary>
    public float health = 50f;

    /// <summary>
    /// Called once per frame, handles enemy behavior such as detection and shooting.
    /// </summary>
    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Rotate to face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Check if the player is within shooting range
            if (distanceToPlayer <= shootingRange && Time.time >= nextTimeToFire)
            {
                // Shoot at the player
                StartCoroutine(Shoot(direction));
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
    }

    /// <summary>
    /// Reduces the enemy's health by a specified amount and checks if the enemy should die.
    /// </summary>
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles enemy death, spawns health collectibles and destroys then destroys itself
    /// </summary>
    void Die()
    {
        SpawnHealth();
        Destroy(gameObject);
    }

    /// <summary>
    /// Spawns health at the enemy's position.
    /// </summary>
    void SpawnHealth()
    {
        Instantiate(healthToSpawn, transform.position, healthToSpawn.transform.rotation);
    }

    /// <summary>
    /// Shoots a bullet in the specified direction.
    /// </summary>
    IEnumerator Shoot(Vector3 direction)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
            bulletScript.speed = bulletSpeed; // Set the bullet speed
        }

        // Optionally, add any shooting animation or effects here

        yield return null;
    }
}




