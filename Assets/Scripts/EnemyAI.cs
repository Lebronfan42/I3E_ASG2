using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
	private GameObject healthToSpawn;

    public Transform player; // Reference to the player
    public float detectionRange = 20f; // Detection range for the enemy
    public float shootingRange = 15f; // Range within which the enemy can shoot
    public float fireRate = 1f; // Time between shots
    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public Transform firePoint; // Point from which bullets are fired
    public float bulletSpeed = 10f; // Speed of the bullet

    private float nextTimeToFire = 0f; // Time until the next shot can be fired

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

    public float health = 50f;

	public void TakeDamage (float amount)
	{
		health -= amount;
		if (health <=0f)
		{
			Die();
		}
	}

	void Die()
	{
        SpawnHealth();
		Destroy (gameObject);
	}

    void SpawnHealth()
	{
		Instantiate(healthToSpawn, transform.position, healthToSpawn.transform.rotation);
		
	}

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




