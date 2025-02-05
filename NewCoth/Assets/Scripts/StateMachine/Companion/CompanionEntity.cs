using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionEntity : MonoBehaviour
{
    public Animator anim;
    public CompanionFiniteStateMachine stateMachine { get; private set; }
    public GameObject modelGO { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public Transform target { get; private set; }

    [Header("Components")]
    public CompanionEntityData entityData;
    public CompanionStateData stateData;

    [Header("Check-Surroundings")]
    public Transform checkPlayerPos;

    public virtual void Start()
    {
        //anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        modelGO = transform.Find("Model").gameObject;
        target = FindObjectOfType<PlayerEntity>().transform; //set target

        stateMachine = new CompanionFiniteStateMachine();

        //Initiaize States
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void StopMovement()
    {
        navMeshAgent.ResetPath();  // Clear the path
        navMeshAgent.velocity = Vector3.zero;  // Stop any residual motion
    }

    public virtual void ChasePlayer()
    {
        navMeshAgent.SetDestination(target.position);
    }

    public virtual void CheckPlayerINMaxRange()
    {

    }

    public virtual bool IsPlayerInMinRange()
    {
        bool isPlayerInMinRange = Physics.CheckSphere(checkPlayerPos.position, entityData.checkPlayerInMinRange, entityData.whatIsPlayer);

        return isPlayerInMinRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPlayerPos.position, entityData.checkPlayerInMinRange);
    }
}
