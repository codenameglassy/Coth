using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeForwardState : PlayerState
{
    public PlayerDodgeForwardState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(false);
        entity.SetDodge(true);
    }

    public override void Exit()
    {
      
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.DodgeForward();

        if (Time.time >= startTime + stateData.dodgeTime)
        {
            entity.SetDodge(false);
            entity.stateMachine.ChangeState(entity.idleState);
        }
        else
        {
           
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
