using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour, IInteractable
{
    public float health = 1.0f;
    public GameObject deathEffect;
    public GameObject enemy;
    [SerializeField] private string _prompt;

    //Disable Detection UI effects if Enemy is killed
    public AudioSource audioNormal;
    public AudioSource audioDetected;
    public Image screenFlash;
    public GameObject music;

    //private EnemyVision sight;

    public string InterationPrompt => _prompt;
    public GameObject InteractableGameObject => gameObject;


    //private void Update()
    //{
    //    if (sight.inSight == true && sight.isPlayerDetected == true)
    //   {
    //       health = 999.0f;
    //    }
    //    else
    //    {
    //        health = 1.0f;
    //    }
    // }
    private void Start()
    {
        music = GameObject.Find("Music");
        audioNormal = music.GetComponents<AudioSource>()[0];
        audioDetected = music.GetComponents<AudioSource>()[1];
        screenFlash = GameObject.Find("FlashImage")?.GetComponent<Image>();
    }

    public bool Interact(Interactor interactor)
    {
            takeDamage(10.0f);
            return true;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Destroy(enemy.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            if (EnemyVision2.isScreenFlashRunning == true)
            {
                screenFlash.enabled = false;
                EnemyVision2.isScreenFlashRunning = false;
            }
            if (EnemyVision2.NormalAudio == false)
            {
                audioDetected.enabled = false;
                audioNormal.enabled = true;
                EnemyVision2.NormalAudio = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            takeDamage(1.0f);
        }
    }
}
