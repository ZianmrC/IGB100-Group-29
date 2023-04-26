using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IInteractable
{
    public float health = 1.0f;
    public GameObject deathEffect;

    [SerializeField] private string _prompt;

    //private EnemyVision sight;

    public string InterationPrompt => _prompt;


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

    public bool Interact(Interactor interactor)
    {
            takeDamage(1.0f);
            return true;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
}
