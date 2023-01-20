using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy is patrolling");
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        //patrol code

        //if (sound is detected)
        //{then enemy.SwitchState(enemy.AlertState);}

        if(Input.GetKeyDown(KeyCode.Q))
        {
            enemy.SwitchState(enemy.AlertState);
        }

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
       
    }
}
