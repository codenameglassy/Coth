using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaffInteractState : PlayerState
{
    public PlayerStaffInteractState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(false);
        entity.SpawnPrayVfxRoutine();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.staffInteractTime)
        {
            entity.Unfuse();
            entity.stateMachine.ChangeState(entity.idleState_Staff);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
