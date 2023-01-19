using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State
{
    public AgroState agroState;
    public PatrolState patrolState;
    public bool playerSpotted;


    public override State RunCurrentState()
    {

        if(playerSpotted)
        {
            return agroState;
        
        }
        /*
        
        if (enemy didnt find player after a certain time)
        { 
            return patrolState;
        
        }


        */
        else
        {
            return this;
        }
    }
}
