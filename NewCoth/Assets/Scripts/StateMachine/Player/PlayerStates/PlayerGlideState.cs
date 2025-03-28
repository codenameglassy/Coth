using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlideState : PlayerState
{
    public PlayerGlideState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData, string animBoolName) : base(entity, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Gliding");
        AudioManagerCS.instance.Play("glide");
        entity.GetThirdPersonController().ResetTerminalVelocity();
        PlayerManaManager.instance.RemoveMana(25);
      
    }

    public override void Exit()
    {
        AudioManagerCS.instance.Play("boyLand");
       
        base.Exit();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.HandleGlide();
        entity.CheckGroundWhileGliding();

       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
