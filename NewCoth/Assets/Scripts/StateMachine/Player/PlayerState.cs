using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerFiniteStateMachine stateMachine;
    protected PlayerEntity entity;
    protected PlayerStateData stateData;

    protected float startTime;
    protected string animBoolName;

    public PlayerState(PlayerEntity entity, PlayerFiniteStateMachine stateMachine, PlayerStateData stateData ,string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.stateData = stateData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual  void PhysicsUpdate()
    {

    }
}
