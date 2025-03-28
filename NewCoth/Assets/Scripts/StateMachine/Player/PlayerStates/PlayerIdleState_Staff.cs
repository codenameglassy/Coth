using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState_Staff : PlayerState
{
    public PlayerIdleState_Staff(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(true);
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.InputSwitchToUnarm();
        entity.HandleStaffInteractInput();
        entity.DodgeForward();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            entity.InfuseWater();
        }

        if (Input.GetMouseButtonDown(0))
        {
            entity.stateMachine.ChangeState(entity.attackState);
        }

        if (Input.GetMouseButtonDown(1))
        {
            entity.stateMachine.ChangeState(entity.rangeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
