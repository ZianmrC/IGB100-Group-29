using UnityEngine;

public class EnemyRangedAttack2 : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab to instantiate
    public Transform projectileSpawnPoint; // Spawn point of the projectile
    public float projectileSpeed = 10.0f; // Speed of the projectile
    public float fireRate = 1.0f; // Rate of fire
    public float offsetTarget;
    public AudioClip soundClip; // Sound clip to play when projectile is instantiated

    private float timeSinceLastAttack; // Time elapsed since last attack
    private EnemyPatrol2 enemyPatrol; // Reference to the EnemyPatrol script

    // Start is called before the first frame update
    void Start()
    {
        // Get the references to the other scripts used
        enemyPatrol = GetComponent<EnemyPatrol2>();
        // Initialize timeSinceLastAttack to fireRate to allow immediate attack
        timeSinceLastAttack = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy can attack
        if (enemyPatrol != null)
        {
            // Update timeSinceLastAttack
            timeSinceLastAttack += Time.deltaTime;

            // Check if enough time has elapsed since last attack
            if (timeSinceLastAttack >= fireRate && enemyPatrol.canAttack == true)
            {
                // Instantiate the projectile
                InstantiateProjectile();

                // Reset timeSinceLastAttack
                timeSinceLastAttack = 0.0f;

                // Reset the canAttack flag in EnemyPatrol script
                enemyPatrol.canAttack = false;
            }
        }
    }

    void InstantiateProjectile()
    {
        // Calculate the direction towards player
        Vector3 targetPosition = GameObject.Find("PlayerCapsule").transform.position;
        Vector3 targetPositionWithOffset = new Vector3(targetPosition.x, targetPosition.y + offsetTarget, targetPosition.z); // Add an offset of 0.5 units in the y-axis
        Vector3 direction = (targetPositionWithOffset - projectileSpawnPoint.position).normalized;

        // Instantiate the projectile and set its direction and speed
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = direction * projectileSpeed;

        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

        // Play the sound clip
        if (soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
        }

        // Destroy the projectile after a certain time
        Destroy(projectile, 5.0f); // Change 5.0f to the desired time for projectile destruction
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if colliding object has tag 'Environment' or 'Player'
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Player"))
        {
            // Destroy the projectile upon collision with 'Environment' or 'Player'
            Destroy(gameObject);
        }
    }
}
