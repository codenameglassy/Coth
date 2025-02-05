using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControableEntityState 
{
    protected ControableEntityFiniteStateMachine stateMachine;
    protected ControableEntity entity;
    protected ControableEntityStateData stateData;

    protected float startTime;
    protected string animBoolName;

    public ControableEntityState(ControableEntity entity, ControableEntityFiniteStateMachine stateMachine, ControableEntityStateData stateData, string animBoolName)
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
