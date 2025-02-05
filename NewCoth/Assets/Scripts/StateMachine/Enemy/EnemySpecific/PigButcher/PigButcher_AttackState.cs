using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigButcher_AttackState : EnemyAttackState
{
    private PigButcherEnemy enemy;
    public PigButcher_AttackState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, PigButcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StopMovement();
        enemy.FaceThis(enemy.target.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.attackTime)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
