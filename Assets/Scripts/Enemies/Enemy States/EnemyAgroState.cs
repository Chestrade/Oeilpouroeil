using UnityEngine;

public class EnemyAgroState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("J'essaye de te tuer mon calice");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}

