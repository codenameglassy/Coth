using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigButcherEnemy : EnemyEntity
{
    public PigButcher_IdleState idleState { get; private set; }
    public PigButcher_PatrolState patrolState { get; private set; }
    public PigButcher_ChaseState chaseState { get; private set; }
    public PigButcher_AttackState attackState { get; private set; }

    [Header("State Data")]
    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyPatrolState patrolStateData;
    [SerializeField] private D_EnemyChaseState chaseStateData;
    [SerializeField] private D_EnemyAttackState attackStateData;   
    public override void Start()
    {
        base.Start();

        idleState = new PigButcher_IdleState(this, stateMachine, "idle", idleStateData, this);
        patrolState = new PigButcher_PatrolState(this, stateMachine, "patrol", patrolStateData, this);
        chaseState = new PigButcher_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        attackState = new PigButcher_AttackState(this, stateMachine, "attack", attackStateData, this);

        stateMachine.Initialize(idleState);
    }
}
