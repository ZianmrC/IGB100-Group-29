using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float range = 100f;
    public int damage = 10;
    public float fireRate = 2f;
    public float projectileVelocity;
    public float upwardForce;
    public GameObject gun;
    public AudioClip gunShot;
    public GameObject muzzleFlash;

    public float recoilRotation = 30f; // Adjust this value to control the recoil rotation
    public float recoilDuration = 0.2f;
    public float returnSpeed = 5f;

    private float nextFireTime;
    private Quaternion originalGunRotation;

    void Start()
    {
        originalGunRotation = gun.transform.localRotation;
    }

    void Update()
    {
        if (gun.activeSelf)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        AudioSource.PlayClipAtPoint(gunShot, transform.position);
        muzzleFlash.SetActive(true);
        StartCoroutine(DeactivateMuzzleFlash());

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Vector3 bulletVelocity = firePoint.TransformDirection(Vector3.forward) * projectileVelocity;
        bulletVelocity += Vector3.up * upwardForce;
        bulletRigidbody.velocity = bulletVelocity;

        bullet.GetComponent<MeshRenderer>().material.color = Color.red;

        Destroy(bullet, 1f);

        StartCoroutine(RecoilGun());
    }

    private IEnumerator DeactivateMuzzleFlash()
    {
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);
    }

    private IEnumerator RecoilGun()
    {
        Quaternion targetRotation = Quaternion.Euler(-recoilRotation, 0f, 0f); // Rotate around the x-axis
        float timer = 0f;

        while (timer < recoilDuration)
        {
            timer += Time.deltaTime;
            gun.transform.localRotation = Quaternion.Lerp(originalGunRotation, targetRotation, timer / recoilDuration);
            yield return null;
        }

        timer = 0f;

        while (timer < recoilDuration)
        {
            timer += Time.deltaTime;
            gun.transform.localRotation = Quaternion.Lerp(targetRotation, originalGunRotation, timer / recoilDuration);
            yield return null;
        }
    }

}
