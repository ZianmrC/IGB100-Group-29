using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float fieldOfVisionAngle; // Field of vision angle in degrees
    public float detectionDistance; // Maximum detection distance of the enemy
    public bool isPlayerDetected = false;
    public bool inSight = false; // New bool variable to detect if there is no obstruction between player and enemy

    public Transform shootPoint; // Reference to the enemy's shoot point
    public float shootDetectionDistance; // Maximum detection distance for shooting

    private Transform player; // Reference to the player's transform
    MusicPlayer musicPlayer;

    void Start()
    {
        // Get reference to the player's transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        //musicPlayer = GameObject.Find("UI_EventSystem").GetComponent<MusicPlayer> ();
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
                    inSight = true; // Set inSight to true
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
                inSight = true; // Set inSight to true
            }

            // Check if there is no obstruction between enemy's shoot point and player
            Vector3 directionToShootPoint = player.position - shootPoint.position;
            if (Physics.Raycast(shootPoint.position, directionToShootPoint, out hit, shootDetectionDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // Player is within shoot detection distance and in sight of the shoot point, set inSight to true
                    inSight = true;
                }
                else
                {
                    // Player is obstructed from the shoot point, set inSight to false
                    inSight = false;
                }
            }
            else
            {
                // Player is within shoot detection distance and in sight of the shoot point, set inSight to true
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
