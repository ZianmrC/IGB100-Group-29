using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPatrol2 : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of patrol points
    public float normalMoveSpeed = 2.0f; // Normal movement speed of the enemy
    public float detectedMoveSpeedDelta = 2.0f; // Delta movement speed of the enemy when player is detected
    public float normalAngularSpeed = 120.0f; // Normal angular speed of the enemy
    public float detectedAngularSpeedDelta = 120.0f; // Delta angular speed of the enemy when player is detected
    private int currentPatrolPointIndex = 0; // Current index of the patrol point
    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent component
    private Transform player; // Reference to player object
    private EnemyVision2 enemyVision; // Reference to EnemyVision script
    public bool canAttack; // Whether the enemy can attack the player or not
    private Transform playerShootSpot;
    public bool aware; // Bool to check if the enemy is 'in combat' or patrolling
    public bool shooting;
    public GameObject gun;
    private Animator animator;
    public bool isPatrolling;
    private float playerDetectionTimer = 0f;
    private float stationaryTime;

    //Only for Enemies in the Start Screen
    public bool StartScreenEnemy; //Bool only true for enemies in the start screen
    public bool gunVisible; //Makes gun visible
    public bool pistolWalking;

    // Start is called before the first frame update
    void Start()
    {
        aware = false;
        shooting = false;
        // Get reference to NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gun = transform.Find("M1911 Handgun_Black").gameObject;

        // Set initial target to the first patrol point
        SetTarget(patrolPoints[currentPatrolPointIndex]);

        // Replace "player" with your actual player object reference
        player = GameObject.Find("PlayerCapsule").transform;
        if (player == null)
        {
            Patrolling();
        }

        // Get reference to EnemyVision script
        enemyVision = GetComponent<EnemyVision2>();
        playerShootSpot = GameObject.Find("InteractionPoint").transform;
    }

    void Update()
    {
        if (StartScreenEnemy == true)
        {
            Patrolling();
            gun.SetActive(gunVisible);
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Vector3 lookAtPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            Vector3 offset = new Vector3(0f, 0f, 0f); //Offset lookat function

            //If Player is detected and is within attack range
            if (enemyVision.canAttack == true && enemyVision.isPlayerDetected == true)
            {
                aware = true;
                canAttack = true;
                shooting = true;
                navMeshAgent.isStopped = true;

                transform.LookAt(lookAtPosition + offset);
            }
            //If player is detected and not within attack range
            else if (enemyVision.isPlayerDetected == true)
            {
                aware = true;
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = normalMoveSpeed + detectedMoveSpeedDelta;
                navMeshAgent.angularSpeed = normalAngularSpeed + detectedAngularSpeedDelta;
            }
            //Player is undetected, resume patrolling
            else
            {
                navMeshAgent.speed = normalMoveSpeed;
                navMeshAgent.angularSpeed = normalAngularSpeed;

                Patrolling(); // Always call the Patrolling() method when the player is not detected

                // If the enemy was previously aware of the player's presence, move towards the last known position
                if (aware)
                {
                    navMeshAgent.SetDestination(enemyVision.lastKnownPosition);
                    if (Vector3.Distance(transform.position, enemyVision.lastKnownPosition) < 0.1f)
                    {
                        aware = false;
                    }
                }
            }
            bool NotStationary = navMeshAgent.velocity.magnitude > 0;
            gun.SetActive(aware);
            animator.SetBool("IsAware", aware);
            animator.SetBool("NotStationary", NotStationary);

            if (NotStationary)
            {
                stationaryTime = 0f;
            }
            else
            {
                stationaryTime += Time.deltaTime;
                if (stationaryTime > 6f)
                {
                    navMeshAgent.isStopped = false;
                    SetTarget(patrolPoints[currentPatrolPointIndex]);
                }
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(6f);
        navMeshAgent.isStopped = false;
        Patrolling();
    }

    void Patrolling()
    {
        if (navMeshAgent.remainingDistance < 0.1f)
        {
            // Set the next target
            currentPatrolPointIndex = (currentPatrolPointIndex + 1);
            if (currentPatrolPointIndex >= patrolPoints.Length) currentPatrolPointIndex = 0;
            SetTarget(patrolPoints[currentPatrolPointIndex]);
            isPatrolling = true; // Set the boolean flag to true to indicate that patrolling is in progress
            gun.SetActive(false);
            aware = false;
        }
        else if (navMeshAgent.remainingDistance >= 0.1f)
        {
            isPatrolling = false; // Set the boolean flag to false when enemy is moving towards patrol point
        }
        else if (navMeshAgent.remainingDistance < 0.1 && isPatrolling) isPatrolling = false;

    }
    // Instructs enemy to move towards target
    void SetTarget(Transform target)
    {
        // Set the NavMeshAgent destination to the target position
        if (navMeshAgent.isActiveAndEnabled && NavMesh.SamplePosition(target.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }
}
