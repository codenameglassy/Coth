using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_MoveState : EnemyPatrolState
{
    private Tiger enemy;
    public Tiger_MoveState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyPatrolState stateData, Tiger enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.GoToNextPatrolPoint();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (enemy.HasReachedDestination())
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
