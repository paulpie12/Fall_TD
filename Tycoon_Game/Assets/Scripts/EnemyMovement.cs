using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints = new List<Transform>();

    private NavMeshAgent agent;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (wayPoints.Count > 0 )
        {
            agent.SetDestination(wayPoints[0].position);
        }
    }

    private void Update()
    {
        if (wayPoints.Count > 0)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = wayPoints[0].position;
            float distance = Vector2.Distance(
        new Vector2(pos.x, pos.z),
        new Vector2(targetPos.x, targetPos.z)
        );
            if (distance < 0.5f)
            {
                wayPoints.RemoveAt(0);
                if (wayPoints.Count > 0)
                {
                    agent.SetDestination(wayPoints[0].position);
                }
            }

        }
    }
}
