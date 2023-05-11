using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;         // Prefab of the bullet to shoot
    public Transform firePoint;            // Transform representing the position where the bullet should be spawned
    public float range = 100f;             // Maximum range of the bullet
    public int damage = 10;               // Damage of the bullet
    public float fireRate = 2f;         // Rate of fire in seconds (e.g. 0.1f means 10 bullets per second)
    public float projectileVelocity = 50f; // Velocity of the bullet
    public GameObject gun;
    public bool gunVisibility; // Allows gun to be visisble

    private float nextFireTime;          // Time of the next allowed shot

    void Start()
    {
        gun.SetActive(gunVisibility);
    }

    void Update()
    {
        if (gunVisibility == true)
        {
            // Check for fire input (e.g. left mouse button)
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                // Shoot a bullet
                Shoot();
                // Update the next allowed shot time
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // Instantiate a new bullet from the bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set bullet properties directly on the bullet object
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Set collision detection mode to ContinuousDynamic
        bulletRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        bulletRigidbody.velocity = firePoint.TransformDirection(Vector3.forward) * projectileVelocity; // Set bullet velocity based on projectileVelocity
        bullet.GetComponent<MeshRenderer>().material.color = Color.red; // Change bullet color to red

        // Destroy the bullet after a certain time (optional)
        Destroy(bullet, 5f);
    }


}
