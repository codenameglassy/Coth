using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : EnemyEntity
{
    public Tiger_IdleState idleState { get; private set; }
    public Tiger_MoveState moveState { get; private set; }

    public D_EnemyIdleState idleStateData;
    public D_EnemyPatrolState moveStateData;

    public override void Start()
    {
        base.Start();

        idleState = new Tiger_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Tiger_MoveState(this, stateMachine, "move", moveStateData, this);

        stateMachine.Initialize(idleState);
    }
}
