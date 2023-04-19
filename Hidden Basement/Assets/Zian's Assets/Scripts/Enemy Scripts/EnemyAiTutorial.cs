using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    /*
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSIghtRange, plauyerInAttackRange;

    private void Awake()
    {
        plyaer = GameObject.Find("Player".transform);
        agent = GetComponent<NavMeshAgent>();
    }
    private void Patrolling()
    {
        if (walkPointSet) SearchWalkPoint();
    }
    private void ChasePlayer()
    {

    }
    private void AttackPlayer()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight and attack range
        playerInSIghtRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSIghtRange & !playerInAttackRange) Patrolling();
        if (playerInSIghtRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSIghtRange) AttackPlayer();
    }
    */
}
