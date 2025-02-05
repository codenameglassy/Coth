using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionState 
{
    protected CompanionFiniteStateMachine stateMachine;
    protected CompanionEntity entity;
    protected CompanionStateData stateData;

    protected float startTime;
    protected string animBoolName;

    public CompanionState(CompanionEntity entity, CompanionFiniteStateMachine stateMachine, CompanionStateData stateData, string animBoolName)
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

    public virtual void PhysicsUpdate()
    {

    }
}
