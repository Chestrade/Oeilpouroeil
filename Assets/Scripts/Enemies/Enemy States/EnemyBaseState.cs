using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy); //Appelé une fois que l'enemi entre dans un nouveau state
 
    public abstract void UpdateState(EnemyStateManager enemy); // comme un void update a l'interieur du state

    public abstract void OnCollisionEnter(EnemyStateManager enemy, Collision collision); //

}
