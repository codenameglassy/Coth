using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(true);

        //setup
        entity.staff.SetActive(false);
        entity.Unfuse();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.InputSwitchToStaff();

        entity.HandlePrayInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            entity.stateMachine.ChangeState(entity.castControlState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.stateMachine.ChangeState(entity.dodgeForwardState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!entity.CheckEnemyInRange())
            {
                Debug.Log("No enemy in range!");
                return;
            }
            entity.stateMachine.ChangeState(entity.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
