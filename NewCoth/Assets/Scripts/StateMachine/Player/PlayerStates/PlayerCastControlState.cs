using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastControlState : PlayerState
{
    public PlayerCastControlState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(false);
        //entity.SpawnPrayVfxRoutine();
        //entity.SpawnCastControlVfx();
        //entity.SmoothZoom(2f, entity.zoomTime);
        AudioManagerCS.instance.Play("chime");

        ControableEntityManager.instance.SpawnWave();
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.castControlTime)
        {
           
         
          //  stateMachine.ChangeState(entity.waitState);
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
