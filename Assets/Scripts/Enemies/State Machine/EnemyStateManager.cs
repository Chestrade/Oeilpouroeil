using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState; //reference to the active state of the state machine

    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyAlertState AlertState = new EnemyAlertState();
    public EnemyAgroState AgroState = new EnemyAgroState();

   
    void Start()
    {
        currentState = PatrolState;

        currentState.EnterState(this); //reference to the context (this EXACT Monobehavior script)
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
