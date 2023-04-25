using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of patrol points
    private float attackRange; // Minimum range for attacking the player
    public float normalMoveSpeed = 2.0f; // Normal movement speed of the enemy
    public float detectedMoveSpeedDelta = 2.0f; // Delta movement speed of the enemy when player is detected
    public float normalAngularSpeed = 120.0f; // Normal angular speed of the enemy
    public float detectedAngularSpeedDelta = 120.0f; // Delta angular speed of the enemy when player is detected
    private int currentPatrolPointIndex = 0; // Current index of the patrol point
    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent component
    private Transform player; // Reference to player object
    private EnemyVision enemyVision; // Reference to EnemyVision script
    public bool canAttack; // Whether the enemy can attack the player or not
    private Transform playerShootSpot;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set initial target to the first patrol point
        SetTarget(patrolPoints[currentPatrolPointIndex]);

        // Replace "player" with your actual player object reference
        player = GameObject.Find("PlayerCapsule").transform;

        // Get reference to EnemyVision script
        enemyVision = GetComponent<EnemyVision>();
        attackRange = enemyVision.shootDetectionDistance;
        playerShootSpot = GameObject.Find("InteractionPoint").transform;
    }

    void Update()
    {
        // Check if the enemy is within attack range of the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && enemyVision.inSight)
        {
            // If player is within attack range, stop moving but still face towards the player
            canAttack = true;
            navMeshAgent.isStopped = true;
            transform.LookAt(playerShootSpot);
        }
        else
        {
            // If player is not within attack range
            if (enemyVision.isPlayerDetected)
            {
                // If player is detected, move towards the player with increased speed and angular speed
                navMeshAgent.speed = normalMoveSpeed + detectedMoveSpeedDelta;
                navMeshAgent.angularSpeed = normalAngularSpeed + detectedAngularSpeedDelta;
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // If player is not detected, continue patrolling with normal speed and angular speed
                navMeshAgent.speed = normalMoveSpeed;
                navMeshAgent.angularSpeed = normalAngularSpeed;
                Patrolling(); // Call the Patrolling() method to handle patrol behavior
            }

            canAttack = false;
            navMeshAgent.isStopped = false; // Resume movement
        }

        // Check if the enemy is not within attack range and player is not detected, then resume patrolling
        if (distanceToPlayer > attackRange && !enemyVision.isPlayerDetected)
        {
            Patrolling();
        }
    }
    private bool isPatrolling = false;

    void Patrolling()
    {
        if (navMeshAgent.remainingDistance < 0.1f && !isPatrolling)
        {
            // Set the next target
            currentPatrolPointIndex = (currentPatrolPointIndex + 1);
            if (currentPatrolPointIndex >= patrolPoints.Length) currentPatrolPointIndex = 0;
            SetTarget(patrolPoints[currentPatrolPointIndex]);
            isPatrolling = true; // Set the boolean flag to true to indicate that patrolling is in progress
        }
        else if (navMeshAgent.remainingDistance >= 0.1f)
        {
            isPatrolling = false; // Set the boolean flag to false when enemy is moving towards patrol point
        }
        else if (navMeshAgent.remainingDistance < 0.1 && isPatrolling) isPatrolling = false;

    }



    // Set the target for the enemy to move towards
    void SetTarget(Transform target)
    {
        // Set the NavMeshAgent destination to the target position
        if (navMeshAgent.isActiveAndEnabled && NavMesh.SamplePosition(target.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }
}
