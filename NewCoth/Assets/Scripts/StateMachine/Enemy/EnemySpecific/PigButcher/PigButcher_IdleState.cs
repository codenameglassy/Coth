using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigButcher_IdleState : EnemyIdleState
{
    private PigButcherEnemy enemy;
    public PigButcher_IdleState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, PigButcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

       /* if (enemy.IsCheckPlayerInMaxDistance())
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
            return;
        }*/

        if(Time.time >= startTime + stateData.idleTime)
        {
            if (enemy.IsCheckPlayerInMaxDistance())
            {
                enemy.stateMachine.ChangeState(enemy.chaseState);
               
            }
            else
            {
                enemy.stateMachine.ChangeState(enemy.patrolState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
