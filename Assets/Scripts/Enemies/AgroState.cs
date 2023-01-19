using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroState : State
{
    public PatrolState patrolState;
    public bool playerKilled;
    public bool playerEscaped;

    public override State RunCurrentState()
    {
        if(playerKilled || playerEscaped)
        {
            return patrolState;
        }

        else
        {
            return this;
        }
    }
}
