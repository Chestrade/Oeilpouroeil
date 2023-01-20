using UnityEngine;

public class EnemyAgroState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy is Agro");
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemy.SwitchState(enemy.PatrolState);
        }
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
