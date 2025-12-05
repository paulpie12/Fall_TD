using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TowerType
{
    Sniper,
    Rapid,
    Basic
}

public class TowerLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();
    [SerializeField] private float range = 4f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private Gun gun;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float damage = 10;
    [SerializeField] private Button DamageButton;
    [SerializeField] private Button RangeButton;

    [Header("Text for Buttons")]
    [SerializeField] private Text damageButtonText;
    [SerializeField] private Text rangeButtonText;

    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private TowerType towertype;

    public int damageLevel;
    public bool Placed = true;

    private float shootTimer = 0f;
    private int rangeLevel = 0;
    private int damagelevel = 0;
    private PointSystem pointsystem;
    private int damageUpgradeCost = 0;
    private int rangeUpgradeCost = 0;

    private RangeCircle rangeCircle;

    private void Awake()
    {
        DamageButton.gameObject.SetActive(false);
        RangeButton.gameObject.SetActive(false);

        DamageButton.onClick.AddListener(() => upgradeSystem.DamageUpgrade(this));
        RangeButton.onClick.AddListener(() => upgradeSystem.RangeUpgrade(this));
    }

    private void Start()
    {
        rangeCircle = GetComponent<RangeCircle>();
        if (rangeCircle != null)
            rangeCircle.UpdateRadius(range);

        pointsystem = PointSystem.instance;

        // Starting upgrade cost
        if (towertype == TowerType.Basic)
        {
            damageUpgradeCost = 60;
            rangeUpgradeCost = 60;
        }
        else if (towertype == TowerType.Sniper)
        {
            damageUpgradeCost = 90;
            rangeUpgradeCost = 90;
        }
        else if (towertype == TowerType.Rapid)
        {
            damageUpgradeCost = 120;
            rangeUpgradeCost = 120;
        }
        else
        {
            Debug.Log("Error setting starting upgrade cost");
        }
    }

    private void Update()
    {
        UpdateEnemiesInRange();

        if (enemiesInRange.Count > 0)
        {
            this.transform.LookAt(enemiesInRange[0].transform);

            shootTimer += Time.deltaTime;

            if (shootTimer >= delay && Placed)
            {
                gun.Shoot(damage);
                shootTimer = 0f;
            }
        }
    }

    public void SetUpgradeSystem(UpgradeSystem system)
    {
        upgradeSystem = system;

        upgradeSystem.OnDamageClick += (tower) =>
        {
            if (tower == this) UpgradeDamage();
        };

        upgradeSystem.OnRangeClick += (tower) =>
        {
            if (tower == this) UpgradeRange();
        };
    }

    private void UpdateEnemiesInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, detectionLayer);

        List<GameObject> currentEnemies = new List<GameObject>();

        foreach (Collider hit in hits)
        {
            GameObject enemy = hit.gameObject;

            if (!currentEnemies.Contains(enemy))
                currentEnemies.Add(enemy);

            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
                Debug.Log("Enemy entered range: " + enemy.name);
            }
        }

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }

    private void OnMouseDown()
    {
        if (rangeCircle != null)
        {
            bool currentlyEnabled = rangeCircle.GetComponent<LineRenderer>().enabled;
            rangeCircle.ShowCircle(!currentlyEnabled);
        }

        if (Placed)
        {
            bool showButtons = !DamageButton.gameObject.activeSelf;

            DamageButton.gameObject.SetActive(showButtons);
            RangeButton.gameObject.SetActive(showButtons);

            if (showButtons)
                UpdateButtonText();
        }
    }

 
    private void UpdateButtonText()
    {
        damageButtonText.text = $"Damage + (Cost: {damageUpgradeCost})";
        rangeButtonText.text = $"Range + (Cost: {rangeUpgradeCost})";
    }

    private void UpgradeDamage()
    {
        if (towertype == TowerType.Sniper && damagelevel != 3 && pointsystem.totalPoints >= damageUpgradeCost)
        {
            damage += 5;
            damagelevel += 1;
            pointsystem.RemovePoints(damageUpgradeCost);
            damageUpgradeCost += 25;
        }
        else if (towertype == TowerType.Basic && damagelevel != 3 && pointsystem.totalPoints >= damageUpgradeCost)
        {
            damage += 5;
            damagelevel += 1;
            pointsystem.RemovePoints(damageUpgradeCost);
            damageUpgradeCost += 15;
        }
        else if (towertype == TowerType.Rapid && damagelevel != 3 && pointsystem.totalPoints >= damageUpgradeCost)
        {
            damage += 5;
            damagelevel += 1;
            pointsystem.RemovePoints(damageUpgradeCost);
            damageUpgradeCost += 30;
        }
        else
        {
            Debug.Log("Error upgrading damage");
        }

        UpdateButtonText();
    }

    private void UpgradeRange()
    {
        if (towertype == TowerType.Sniper && rangeLevel != 3 && pointsystem.totalPoints >= rangeUpgradeCost)
        {
            range += 5;
            rangeLevel += 1;
            pointsystem.RemovePoints(rangeUpgradeCost);
            rangeUpgradeCost += 25;
        }
        else if (towertype == TowerType.Basic && rangeLevel != 3 && pointsystem.totalPoints >= rangeUpgradeCost)
        {
            range += 5;
            rangeLevel += 1;
            pointsystem.RemovePoints(rangeUpgradeCost);
            rangeUpgradeCost += 15;
        }
        else if (towertype == TowerType.Rapid && rangeLevel != 3 && pointsystem.totalPoints >= rangeUpgradeCost)
        {
            range += 5;
            rangeLevel += 1;
            pointsystem.RemovePoints(rangeUpgradeCost);
            rangeUpgradeCost += 30;
        }
        else
        {
            Debug.Log("Error upgrading range");
        }

        if (rangeCircle != null)
            rangeCircle.UpdateRadius(range);

        UpdateButtonText();
    }
}
