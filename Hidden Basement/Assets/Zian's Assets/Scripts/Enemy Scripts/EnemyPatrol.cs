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
    public bool aware; // Bool to check if the enemy is 'in combat' or patrolling
    public bool shooting;
    public GameObject gun;
    private Animator animator;

    //Only for Enemies in the Start Screen
    public bool StartScreenEnemy; //Bool only true for enemies in the start screen
    public bool gunVisible; //Makes gun visible
    public bool pistolWalking;

    //For testing Purposes
    public bool test1;
    public bool test2;
    public bool test3;
    public bool test4;
    public bool test5;
    public bool test6;

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
        enemyVision = GetComponent<EnemyVision>();
        attackRange = enemyVision.shootDetectionDistance;
        playerShootSpot = GameObject.Find("InteractionPoint").transform;
    }

    void Update()
    {
        test1 = false;
        test2 = false;
        test3 = false;
        test4 = false;
        test5 = false;
        test6 = false;
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
            if (distanceToPlayer < attackRange)// && enemyVision.inSight)
            {
                canAttack = true;
                shooting = true;
                navMeshAgent.isStopped = true;

                transform.LookAt(lookAtPosition + offset);
                test1 = true;
            }
            //If player is detected and not within attack range
            else if (enemyVision.isPlayerDetected)
            {
                aware = true;
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = normalMoveSpeed + detectedMoveSpeedDelta;
                navMeshAgent.angularSpeed = normalAngularSpeed + detectedAngularSpeedDelta;
                test2 = true;
                //If enemy is within attack range again
                if (distanceToPlayer < attackRange)// && enemyVision.inSight)
                {
                    canAttack = true;
                    shooting = true;
                    navMeshAgent.isStopped = true;
                    transform.LookAt(lookAtPosition + offset);
                    test3 = true;
                }
                //Move towards player if not within attack range
                else if (distanceToPlayer < attackRange)
                {
                    navMeshAgent.SetDestination(player.position);
                    shooting = false;
                    aware = true;
                    gun.SetActive(true);
                    test4 = true;
                }
                //Player is undetected, resume patrolling
                else
                {
                    navMeshAgent.speed = normalMoveSpeed;
                    navMeshAgent.angularSpeed = normalAngularSpeed;
                    Patrolling();
                    test5 = true;
                }
            }
            //Player is undetected, resume patrolling
            /*
            else
            {
                navMeshAgent.speed = normalMoveSpeed;
                navMeshAgent.angularSpeed = normalAngularSpeed;
                Patrolling();
                test5 = true;
            }
            */

            if (distanceToPlayer > attackRange && !enemyVision.isPlayerDetected)
            {
                Patrolling();
                shooting = false;
                test6 = true;
                if (!pistolWalking) gun.SetActive(false);
            }
            gun.SetActive(aware);
            Debug.Log($"test1:{test1}, test2:{test2}, test3:{test3}, test4:{test4}, test5:{test5}, test6:{test6}");
            //Debug.Log(enemyVision.inSight);
            animator.SetBool("IsAware", aware);
            animator.SetBool("IsShooting", shooting);
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
