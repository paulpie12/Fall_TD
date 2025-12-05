using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseLogic : MonoBehaviour
{
    [SerializeField] private float Health = 10;
    [SerializeField] private float MaxHealth = 10;

    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject GameOverCanvas;

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] PointSystem PointSystem;

    private float enemyHealth = 0;

    private void Start()
    {
        GameOverCanvas.SetActive(false);
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
        if(Health <= 0)
        {
            UI.SetActive(false);
            GameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
            ScoreText.text = "Score: " + PointSystem.score;

        }

    }
}
