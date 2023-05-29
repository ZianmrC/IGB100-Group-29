using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class EnemyVision2 : MonoBehaviour
{
    public float viewDistance = 10f; // Distance the enemy can see
    public float viewAngle = 45f; // Angle of the enemy's vision cone
    public float attackRange = 7f;

    private Transform player; // Reference to the player's transform
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public bool isPlayerDetected;
    private float detectionStartTime = 0f;
    public bool canAttack;
    public GameObject music; //Reference to Music GameObject
    public static bool NormalAudio;
    private AudioSource audioNormal;
    private AudioSource audioDetected;
    private bool beenDetected;

    public Image screenFlash;
    public static Vector3 lastKnownPosition;
    public static bool isScreenFlashRunning = false;

    public GameObject enemy;

    //For Mechanic where if player was detected, all enemies on the same floor converge on the player
    public bool floor1Enemy;
    public static bool PlayerDetected; //Bool to share if at least 1 enemy has detected player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        music = GameObject.Find("Music");
        audioNormal = music.GetComponents<AudioSource>()[0];
        audioDetected = music.GetComponents<AudioSource>()[1];
        audioDetected.enabled = false;

        screenFlash = GameObject.Find("FlashImage")?.GetComponent<Image>();
        detectedText = GameObject.Find("Detected Text")?.GetComponent<TextMeshProUGUI>();
        NormalAudio = true;
    }

    private float detectedMusicTimer = 0f;
    private float detectedMusicDuration = 5f;
    private bool hasExecutedCode = false; // to ensure the code is executed only once per 4 seconds
    private float beenDetectedTime = 0f;
    private float debugPrintInterval = 4f;
    private bool hasPrintedDebug = true;
    private float flashDuration = 0.75f;
    private float flashMaxAlpha = 0.3f;

    public TextMeshProUGUI detectedText;

    private IEnumerator ScreenFlashCoroutine()
    {
        screenFlash.enabled = true;
        detectedText.enabled = true;
        while (true)
        {
            float timer = 0f;
            while (timer < flashDuration)
            {
                float alpha = Mathf.Lerp(0, flashMaxAlpha, timer / flashDuration);
                screenFlash.color = new Color(1, 0, 0, alpha);
                detectedText.color = new Color(1, 0, 0, alpha);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            while (timer < flashDuration)
            {
                float alpha = Mathf.Lerp(flashMaxAlpha, 0, timer / flashDuration);
                screenFlash.color = new Color(1, 0, 0, alpha);
                detectedText.color = new Color(1, 0, 0, alpha);
                timer += Time.deltaTime;
                yield return null;
            }

            // Set the flag to false when the coroutine is finished
            isScreenFlashRunning = false;

            // Deactivate the screen flash and detected text
            screenFlash.enabled = false;
            detectedText.enabled = false;

            yield return null;
        }
    }



    private void Update()
    {
        DrawVisionCone();
        // Check if the player is within the view cone and distance
        if (IsPlayerInSight() == true)
        {
            screenFlash.enabled = true;
            detectedText.enabled = true;
            isPlayerDetected = true;
            IsPlayerWithinAttackRange(); // Update canAttack
            lastKnownPosition = player.transform.position; // Update lastKnownPosition
            if (NormalAudio == true)
            {
                Debug.Log("test1");
                audioNormal.enabled = false;
                audioDetected.enabled = true;
                NormalAudio = false;
            }
            beenDetectedTime = Time.time; // Set the time when player is detected
            hasPrintedDebug = false; // Reset the flag
            if (isScreenFlashRunning == false)
            {
                isScreenFlashRunning = true;
                StartCoroutine(ScreenFlashCoroutine());
            }
            PlayerDetected = true;
        }
        else
        {
            PlayerDetected = false;
            isPlayerDetected = false;
            canAttack = false; // Player is not in sight, so cannot attack
                               // Check if enough time has passed since player was detected
            if (Time.time - beenDetectedTime >= detectedMusicDuration && !hasPrintedDebug)
            {
                if (NormalAudio == false)
                {
                    audioNormal.enabled = true;
                    audioDetected.enabled = false;
                    NormalAudio = true;
                }
                hasPrintedDebug = true;
                isScreenFlashRunning = false;
                screenFlash.enabled = false;
                detectedText.enabled = false;
            }
            
        }

        if (!isPlayerDetected && lastKnownPosition != Vector3.zero)
        {
            if (lastKnownPosition == null)
            {
                lastKnownPosition = transform.position;
            }
        }
    }
    private void DrawVisionCone()
    {
        // Calculate the field of view angles
        float halfFOV = viewAngle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

        // Calculate the direction vectors for the left and right rays
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        // Draw the vision cone using three rays (forward, left, and right)
        Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.yellow);
        Debug.DrawRay(transform.position, leftRayDirection * viewDistance, Color.yellow);
        Debug.DrawRay(transform.position, rightRayDirection * viewDistance, Color.yellow);
    }

    private bool IsPlayerInSight()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate the angle between the enemy's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Check if the player is within the view cone and distance
        if (angleToPlayer < viewAngle / 2f && directionToPlayer.magnitude < viewDistance)
        {
            // Check if there are no obstacles blocking the view to the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, directionToPlayer, out hit, viewDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    lastKnownPosition = player.position; // Set lastKnownPosition to the player's position
                    return true;
                }
            }
        }
        return false;
    }

    private void IsPlayerWithinAttackRange()
    {
        // Calculate the distance from the enemy to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Check if the player is within attack range
        if (distanceToPlayer <= attackRange)
        {
            canAttack = true;
        }
        else canAttack = false;
    }

   private void OnDrawGizmosSelected()
    {
        // Draw a wireframe cone to visualize the enemy's vision
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0f, -viewAngle / 2f, 0f) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0f, viewAngle / 2f, 0f) * transform.forward;

        Gizmos.color = new Color(1f, 1f, 0f, 0.5f); // Yellow with transparency
        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);

        // Draw a wireframe sphere to visualize the enemy's attack range
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f); // Green with transparency
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.DrawRay(transform.position, leftBoundary * attackRange);
        Gizmos.DrawRay(transform.position, rightBoundary * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
    }


}
