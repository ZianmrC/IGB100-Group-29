using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Patrol points for Enemy
    public Transform[] spawnPoints;
    private Transform target;
    public GameObject Enemy;
    private int lastInteractedSpawnPoint = 0;
    public float speed;
    public float rotateSpeed;

    //

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lastInteractedSpawnPoint++;
            target = spawnPoints[lastInteractedSpawnPoint];
            if (lastInteractedSpawnPoint > spawnPoints.Length)
            {
                lastInteractedSpawnPoint = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get direction to target
        Vector3 direction = target.position - transform.position;

        // rotate towards target with customizable angle
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotateSpeed);

        // move towards target
        transform.position = Vector3.MoveTowards(target.position, target.position, speed * Time.deltaTime);

    }
}
