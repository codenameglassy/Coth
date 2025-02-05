using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControableEntityFiniteStateMachine
{
    public ControableEntityState currentState { get; private set; }

    public void Initialize(ControableEntityState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(ControableEntityState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
