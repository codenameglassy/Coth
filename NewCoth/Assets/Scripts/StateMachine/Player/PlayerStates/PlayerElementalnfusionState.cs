using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalnfusionState : PlayerState
{
    public PlayerElementalnfusionState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
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

        if(Time.time >= startTime + stateData.infusionTime)
        {
            entity.stateMachine.ChangeState(entity.idleState_Staff);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
