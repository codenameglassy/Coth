using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControableEntityWait : ControableEntityState
{
    public ControableEntityWait(ControableEntity entity, ControableEntityFiniteStateMachine stateMachine, ControableEntityStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
