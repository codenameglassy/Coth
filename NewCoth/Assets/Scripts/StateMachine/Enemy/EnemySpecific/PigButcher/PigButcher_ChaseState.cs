using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigButcher_ChaseState : EnemyChaseState
{
    private PigButcherEnemy enemy;
    public PigButcher_ChaseState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyChaseState stateData, PigButcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (enemy.IsCheckPlayerInMinDistance())
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
            return;
        }
        enemy.SetDestination(enemy.target);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
