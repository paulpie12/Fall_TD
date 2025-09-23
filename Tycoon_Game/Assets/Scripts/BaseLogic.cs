using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLogic : MonoBehaviour
{
    [SerializeField] private int Health = 10;
    [SerializeField] private int MaxHealth = 10;

    [SerializeField] FloatingHealthBar healthBar;

    private int enemyHealth = 0;

    private void Start()
    {
        healthBar.UpdateHealthBar(Health, MaxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) 
            {
                enemyHealth = enemy.CurrentHealth;
                TakeDamage(enemyHealth);
                enemy.Die();
            }
        }
    }

    private void TakeDamage (int damage)
    {
        Health -= damage;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if(Health >= 0)
        {
            //create logic for game lose
        }

    }
}
