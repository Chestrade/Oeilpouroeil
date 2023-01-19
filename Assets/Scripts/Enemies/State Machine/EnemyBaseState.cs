using UnityEngine;
using UnityEngine.AI;


//Enemy AI tutorial https://www.youtube.com/watch?v=c8Nq19gkNfs

public abstract class EnemyBaseState
{
    NavMeshAgent agent;
    [Header("Movement")]
    [SerializeField] public float enemySpeed;
    Vector3 target;

    [Header("Patrol")]
    [SerializeField] public bool isStationnary;
    public Transform[] waypoints;
    int waypointIndex;
    

    

   


    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
    public abstract void OnCollisionEnter(EnemyStateManager enemy,Collision collision); 
}
