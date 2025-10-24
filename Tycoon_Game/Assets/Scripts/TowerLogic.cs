using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();
    [SerializeField] private float range = 4f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private Gun gun;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float damage = 10;
    [SerializeField] Button DamageButton;
    [SerializeField] Button RangeButton;
    [SerializeField] UpgradeSystem upgradeSystem;


    public int damageLevel;



    public bool Placed = true;

    private float shootTimer = 0f;

    private void Awake()
    {
        DamageButton.gameObject.SetActive(false);
        RangeButton.gameObject.SetActive(false);
        DamageButton.onClick.AddListener(() => upgradeSystem.DamageUpgrade());
        RangeButton.onClick.AddListener(() => upgradeSystem.RangeUpgrade());
    }

    private void Update()
    {
        UpdateEnemiesInRange();

        if(enemiesInRange.Count > 0)
        {
            this.transform.LookAt(enemiesInRange[0].transform);

            shootTimer += Time.deltaTime;

            if (shootTimer >= delay && Placed == true)
            {
                gun.Shoot(damage);
                shootTimer = 0f;
            }
        }
    }
    public void SetUpgradeSystem(UpgradeSystem system)
    {
        upgradeSystem = system;
        upgradeSystem.OnDamageClick += UpgradeDamage;
        upgradeSystem.OnRangeClick += UpgradeRange;
    }

    private void UpdateEnemiesInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, detectionLayer);

        // Create a temporary list to store current enemies in range
        List<GameObject> currentEnemies = new List<GameObject>();

        // Loop through all colliders and add valid enemies
        foreach (Collider hit in hits)
        {
            GameObject enemy = hit.gameObject;

            if (!currentEnemies.Contains(enemy))
                currentEnemies.Add(enemy);

            // If the enemy is new, add it to the main list
            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
                Debug.Log("Enemy entered range: " + enemy.name);
            }
        }

        // Remove enemies that are no longer in range
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
                continue;
            }
            if (!currentEnemies.Contains(enemiesInRange[i]))
            {
                Debug.Log("Enemy left range: " + enemiesInRange[i].name);
                RemoveEnemy(enemiesInRange[i]);
            }

        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    private void OnDrawGizmos()
    {
        // Yellow sphere for detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        // Red line showing forward direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
    private void OnMouseDown()
    {
        if (DamageButton.isActiveAndEnabled && Placed == true)
        {
            DamageButton.gameObject.SetActive(false);
            RangeButton.gameObject.SetActive(false);
        }
        else if (!DamageButton.isActiveAndEnabled && Placed == true)
        {
            DamageButton.gameObject.SetActive(true);
            RangeButton.gameObject.SetActive(true);
        }
    }
    private void UpgradeDamage()
    {
        damage += 5;
    }

    private void UpgradeRange()
    {
        range += 5;
    }

}
