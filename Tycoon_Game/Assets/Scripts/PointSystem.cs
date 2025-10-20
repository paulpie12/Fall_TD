using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    public int totalPoints { get; private set; }
    [SerializeField] private Text points;

    private void Start()
    {
        points.text = "Points:" + totalPoints;
    }
    public void AddPoints(int points)
    {
        totalPoints += points;
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        points.text = "Points:" + totalPoints;
    }
}
