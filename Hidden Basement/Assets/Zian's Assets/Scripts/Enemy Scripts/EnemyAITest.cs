
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyAITest : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float coneAngle;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public bool isPatrolling;
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolPointIndex = 0; // Current index of the patrol point
    public GameObject gun;
    public bool aware;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckCapsule(transform.position, transform.position + transform.forward * sightRange, coneAngle, whatIsPlayer);
        playerInAttackRange = Physics.CheckCapsule(transform.position, transform.position + transform.forward * attackRange, coneAngle, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer(); 
            Debug.Log("Attacking Player");
        }
    }

    private void Patroling()
    {
        if (agent.remainingDistance < 0.1f && !isPatrolling)
        {
            // Set the next target
            currentPatrolPointIndex = (currentPatrolPointIndex + 1);
            if (currentPatrolPointIndex >= patrolPoints.Length) currentPatrolPointIndex = 0;
            SetTarget(patrolPoints[currentPatrolPointIndex]);
            isPatrolling = true; // Set the boolean flag to true to indicate that patrolling is in progress
            gun.SetActive(false);
            aware = false;
        }
        else if (agent.remainingDistance >= 0.1f)
        {
            isPatrolling = false; // Set the boolean flag to false when enemy is moving towards patrol point
        }
        else if (agent.remainingDistance < 0.1 && isPatrolling) isPatrolling = false;
    }
    // Instructs enemy to move towards target
    void SetTarget(Transform target)
    {
        // Set the NavMeshAgent destination to the target position
        if (agent.isActiveAndEnabled && NavMesh.SamplePosition(target.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe cone to visualize the detection range
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, coneAngle, sightRange);
        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, coneAngle, attackRange);
        Handles.DrawWireDisc(transform.position + transform.forward * sightRange, transform.forward, 0.2f);
        Handles.DrawWireDisc(transform.position + transform.forward * attackRange, transform.forward, 0.2f);
    }
}
