using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFiniteStateMachine 
{
    public CompanionState currentState { get; private set; }

    public void Initialize(CompanionState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(CompanionState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
