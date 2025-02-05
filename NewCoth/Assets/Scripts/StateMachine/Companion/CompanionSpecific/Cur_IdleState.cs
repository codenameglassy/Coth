using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cur_IdleState : CompanionState
{
    private Cur companion;
    public Cur_IdleState(CompanionEntity entity, CompanionFiniteStateMachine stateMachine, CompanionStateData stateData, string animBoolName, Cur companion) : base(entity, stateMachine, stateData, animBoolName)
    {
        this.companion = companion;
    }

    public override void Enter()
    {
        base.Enter();
        entity.StopMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.idleTime)
        {
            if (!companion.IsPlayerInMinRange())
            {
                companion.stateMachine.ChangeState(companion.chaseState);
            }
         
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
