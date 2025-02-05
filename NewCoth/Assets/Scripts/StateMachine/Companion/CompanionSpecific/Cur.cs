using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cur : CompanionEntity
{
    public Cur_IdleState idlestate { get; private set; }
    public Cur_ChasePlayerState chaseState { get; private set; }

    public override void Start()
    {
        base.Start();

        idlestate = new Cur_IdleState(this, stateMachine, stateData, "idle", this);
        chaseState = new Cur_ChasePlayerState(this, stateMachine, stateData, "chase", this);

        stateMachine.Initialize(idlestate);
    }

    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
