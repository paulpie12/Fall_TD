using System;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public event Action<TowerLogic> OnDamageClick;
    public event Action<TowerLogic> OnRangeClick;
    public void DamageUpgrade(TowerLogic tower)
    {
        OnDamageClick?.Invoke(tower);
    }

    public void RangeUpgrade(TowerLogic tower)
    {
        OnRangeClick?.Invoke(tower);
    }
}
