using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cur_ChasePlayerState : CompanionState
{
    private Cur companion;
    public Cur_ChasePlayerState(CompanionEntity entity, CompanionFiniteStateMachine stateMachine, CompanionStateData stateData, string animBoolName, Cur companion) : base(entity, stateMachine, stateData, animBoolName)
    {
        this.companion = companion;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (companion.IsPlayerInMinRange())
        {
            companion.stateMachine.ChangeState(companion.idlestate);
        }
        else
        {
            entity.ChasePlayer();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
