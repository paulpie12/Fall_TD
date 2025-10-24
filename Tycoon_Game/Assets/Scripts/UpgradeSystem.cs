using System;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public event Action OnDamageClick;
    public event Action OnRangeClick;
    public void DamageUpgrade()
    {
        OnDamageClick.Invoke();
    }

    public void RangeUpgrade()
    {
        OnRangeClick.Invoke();
    }
}
