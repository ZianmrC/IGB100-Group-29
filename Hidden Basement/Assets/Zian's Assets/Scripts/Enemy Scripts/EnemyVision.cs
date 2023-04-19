using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float fieldOfVisionAngle = 60.0f; // Field of vision angle in degrees
    public float detectionDistance = 10.0f; // Maximum detection distance of the enemy
    public bool isPlayerDetected = false;
    public bool inSight = false; // New bool variable to detect if there is no obstruction between player and enemy

    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Get reference to the player's transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within field of vision
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

        // If player is within field of vision angle and within detection distance
        if (angleToPlayer <= fieldOfVisionAngle * 0.5f && directionToPlayer.magnitude <= detectionDistance)
        {
            // Check if there is no obstruction between player and enemy
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // Player is within field of vision and in sight, set both isPlayerDetected and inSight to true
                    isPlayerDetected = true;
                    inSight = true;
                }
                else
                {
                    // Player is within field of vision but obstructed, set isPlayerDetected to true and inSight to false
                    isPlayerDetected = true;
                    inSight = false;
                }
            }
            else
            {
                // Player is within field of vision and in sight, set both isPlayerDetected and inSight to true
                isPlayerDetected = true;
                inSight = true;
            }
        }
        else
        {
            // Player is not within field of vision, set both isPlayerDetected and inSight to false
            isPlayerDetected = false;
            inSight = false;
        }
    }

    // Visualize the field of vision in the scene view
    void OnDrawGizmosSelected()
    {
        // Draw the detection cone using Gizmos
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfVisionAngle * 0.5f, 0) * transform.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfVisionAngle * 0.5f, 0) * transform.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfVisionAngle * 0.5f, 0) * transform.forward * detectionDistance);
    }
}
