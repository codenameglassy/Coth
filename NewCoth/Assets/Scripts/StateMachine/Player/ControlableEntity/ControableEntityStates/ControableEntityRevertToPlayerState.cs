using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControableEntityRevertToPlayerState : ControableEntityState
{
    public ControableEntityRevertToPlayerState(ControableEntity entity, ControableEntityFiniteStateMachine stateMachine, ControableEntityStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
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
        if(Time.time >= startTime + stateData.revertToTplayerTime)
        {
            //entity.ControlPlayer();
            ControableEntityManager.instance.ControlPlayer();
            entity.stateMachine.ChangeState(entity.waitState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
