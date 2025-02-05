using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_ChaseState : EnemyPatrolState
{
    private Tiger enemy;
    public Tiger_ChaseState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyPatrolState stateData, Tiger enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
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
    }
}
