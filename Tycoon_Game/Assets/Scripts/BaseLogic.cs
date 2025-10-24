using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLogic : MonoBehaviour
{
    [SerializeField] private float Health = 10;
    [SerializeField] private float MaxHealth = 10;

    [SerializeField] FloatingHealthBar healthBar;

    private float enemyHealth = 0;

    private void Start()
    {
        healthBar.UpdateHealthBar(Health, MaxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
            if (enemy != null) 
            {
                enemyHealth = enemy.CurrentHealth;
                TakeDamage(enemyHealth);
                enemy.Die();
            }
        }
    }

    private void TakeDamage (float damage)
    {
        Health -= damage;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if(Health >= 0)
        {
            //create logic for game lose
        }

    }
}
