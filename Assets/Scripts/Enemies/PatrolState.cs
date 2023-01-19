using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public AlertState alertState;
    public bool heardSound;
   
    public override State RunCurrentState()
    {
        if (heardSound)
        {
            return alertState;
        }
        else
        {
            return this;
        }
    }
}
