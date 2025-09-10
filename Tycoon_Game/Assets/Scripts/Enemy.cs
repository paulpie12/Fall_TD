using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int Health = 10;
    [SerializeField] private int MaxHealth = 10;

    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        Health = MaxHealth;
        healthBar.UpdateHealthBar(Health, MaxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if (Health < 0) 
        {
            
        }
    }

}
