using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackState : PlayerState
{
    public PlayerRangeAttackState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(false);
        //entity.FaceEnemy();
        entity.RangeAttack();
        PlayerManaManager.instance.RemoveMana(50f);
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
        if(Time.time >= startTime + stateData.rangeAttackTime)
        {
            entity.stateMachine.ChangeState(entity.idleState_Staff);
        }
    }
}
