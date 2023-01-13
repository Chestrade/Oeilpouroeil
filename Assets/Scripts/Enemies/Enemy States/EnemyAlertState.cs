using UnityEngine;

public class EnemyAlertState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Je suis en train de suivre le son");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }
}
