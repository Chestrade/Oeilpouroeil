using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Patrolling AI Tutorial : https://www.youtube.com/watch?v=c8Nq19gkNfs

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [Header("Movement")]
    [SerializeField] public float enemySpeed;

    [Header("Patrol")]
    [SerializeField] public bool isStationnary;
    public Transform[] waypoints;
    int waypointIndex;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
