using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Je suis en train de patrol");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //behavior de l'enemi lorsqu'il patrol

        /*if (soundDetected = true)
        {
            enemy.SwitchState(enemy.AlertState);
        }
        */
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
