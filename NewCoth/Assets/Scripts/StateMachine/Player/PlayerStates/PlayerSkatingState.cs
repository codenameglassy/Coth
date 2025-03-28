using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkatingState : PlayerState
{

    public PlayerSkatingState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.GetThirdPersonController().SetSkating(true);
    }

    public override void Exit()
    {
        entity.GetThirdPersonController().SetSkating(false);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.T))
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }

        if (entity.GetThirdPersonController().GetPlayerVelocity() <= 1f) 
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
