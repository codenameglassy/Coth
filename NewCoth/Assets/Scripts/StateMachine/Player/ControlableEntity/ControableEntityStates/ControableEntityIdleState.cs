using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControableEntityIdleState : ControableEntityState
{
    public ControableEntityIdleState(ControableEntity entity, ControableEntityFiniteStateMachine stateMachine, ControableEntityStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.SetMovement(true);
        entity.SmoothZoom(entity.zoomOutSize, entity.zoomTime);

    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(PlayerManaManager.instance.CurrentMana() <= 0)
        {
            entity.stateMachine.ChangeState(entity.revertToPlayer);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            entity.stateMachine.ChangeState(entity.revertToPlayer);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
