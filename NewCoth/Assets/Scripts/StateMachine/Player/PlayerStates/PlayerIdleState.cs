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
        entity.CheckSwitchToGlideState();

       /* if (Input.GetKeyDown(KeyCode.R))
        {
            entity.stateMachine.ChangeState(entity.castControlState);
        }*/

        if (Input.GetKeyDown(KeyCode.T))
        {
            entity.stateMachine.ChangeState(entity.skatingState);
        }

        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             entity.stateMachine.ChangeState(entity.dodgeForwardState);
         }*/

      /*  if (Input.GetMouseButtonDown(0))
        {
            entity.Chant();
        }

        if (Input.GetMouseButtonDown(1))
        {
            entity.stateMachine.ChangeState(entity.pushingState);
        }*/

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.CheckLoots();
    }
}
