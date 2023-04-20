using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab to instantiate
    public Transform projectileSpawnPoint; // Spawn point of the projectile
    public float projectileSpeed = 10.0f; // Speed of the projectile
    public float fireRate = 1.0f; // Rate of fire
    public float damage; //Damage dealt to player
    private float timeSinceLastAttack; // Time elapsed since last attack
    private EnemyPatrol enemyPatrol; // Reference to the EnemyPatrol script
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the references to the other scripts used
        enemyPatrol = GetComponent<EnemyPatrol>();
        gameManager = GetComponent<GameManager>();
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

    // Instantiate the projectile towards the player
    void InstantiateProjectile()
    {
        // Calculate the direction towards the player
        Vector3 direction = (GameObject.Find("PlayerShootSpot").transform.position - projectileSpawnPoint.position).normalized;

        // Instantiate the projectile and set its direction and speed
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = direction * projectileSpeed;

        // Destroy the projectile after a certain time
        Destroy(projectile, 5.0f); // Change 5.0f to the desired time for projectile destruction
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "Bullet" tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet upon collision with any object
            Destroy(collision.gameObject);
            gameManager.PlayerTakeDamage();
            Debug.Log("Player has taken damage");
        }
    }
}
