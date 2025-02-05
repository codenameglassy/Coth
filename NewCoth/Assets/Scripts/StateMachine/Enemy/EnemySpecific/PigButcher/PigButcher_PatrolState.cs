using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigButcher_PatrolState : EnemyPatrolState
{
    private PigButcherEnemy enemy;
    public PigButcher_PatrolState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyPatrolState stateData, PigButcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (enemy.IsCheckPlayerInMaxDistance())
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
            return;
        }

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
