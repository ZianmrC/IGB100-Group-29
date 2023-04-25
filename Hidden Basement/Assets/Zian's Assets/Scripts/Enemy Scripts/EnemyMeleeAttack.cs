using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackRate = 1.0f; // Rate of fire
    private float timeSinceLastAttack; // Time elapsed since last attack
    private EnemyPatrol enemyPatrol; // Reference to the EnemyPatrol script
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Get the references to the other scripts used
        enemyPatrol = GetComponent<EnemyPatrol>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        // Initialize timeSinceLastAttack to fireRate to allow immediate attack
        timeSinceLastAttack = attackRate;
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
            if (timeSinceLastAttack >= attackRate && enemyPatrol.canAttack == true)
            {
                // Instantiate the projectile
                playerHealth.TakeDamage();
                Debug.Log("test");

                // Reset timeSinceLastAttack
                timeSinceLastAttack = 0.0f;

                // Reset the canAttack flag in EnemyPatrol script
                enemyPatrol.canAttack = false;
            }
        }
    }

}
