using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://www.youtube.com/watch?v=cnpJtheBLLY

public class StateManager : MonoBehaviour
{
    State currentState;
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        
        if (nextState != null)
        {
            SwitchtoNextState(nextState);
        }
    }

    private void SwitchtoNextState(State nextState) //switch le state en haut au suivant
    {
        currentState = nextState;
    }
}
