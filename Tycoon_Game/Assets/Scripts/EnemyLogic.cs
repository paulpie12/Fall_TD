using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float Health = 10;
    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private int PointAmount = 10;

    private PointSystem PointSystem;

    [SerializeField] FloatingHealthBar healthBar;

    public float CurrentHealth => Health;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        PointSystem = FindObjectOfType<PointSystem>();
    }

    private void Start()
    {
        Health = MaxHealth;
        healthBar.UpdateHealthBar(Health, MaxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if (Health <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        PointSystem.AddPoints(PointAmount);
        WaveSpawner spawner = FindObjectOfType<WaveSpawner>();
        if (spawner != null)
        {
            spawner.spawnedEnemies.Remove(gameObject);
        }
        Destroy(gameObject);
    }

}
