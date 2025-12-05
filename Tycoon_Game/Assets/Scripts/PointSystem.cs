using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    //makes this a singleton accessable to all scripts
    public static PointSystem instance {  get; private set; }
    public int totalPoints { get; private set; }
    [SerializeField] private Text points;
    public int score {  get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        totalPoints = 300;
        points.text = "Points:" + totalPoints;
        score = 0;
    }
    public void AddPoints(int points)
    {
        totalPoints += points;
        UpdateTotal();
        score += points;
    }

    public void RemovePoints(int points)
    {
        totalPoints -= points;
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        points.text = "Points:" + totalPoints;
    }
}
