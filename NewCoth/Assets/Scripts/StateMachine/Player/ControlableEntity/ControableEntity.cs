using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ControableEntity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public ControableEntityFiniteStateMachine stateMachine { get; private set; }

    public ControableEntityRevertToPlayerState revertToPlayer { get; private set; }
    public ControableEntityWait waitState { get; private set; }

    public ControableEntityIdleState idleState { get; private set; }

    [Header("Components")]
    [SerializeField] private ControableEntityStateData stateData;
    [SerializeField] private ControableEntityData entityData;
    [SerializeField] private ThirdPersonController thirdPersonController;

    [Header("Check Environment")]
    public GameObject checkDestructablePos;
  
    private void Start()
    {
        anim = GetComponent<Animator>();
        stateMachine = new ControableEntityFiniteStateMachine();

        idleState = new ControableEntityIdleState(this, stateMachine, stateData, "idle");
        revertToPlayer = new ControableEntityRevertToPlayerState(this, stateMachine, stateData, "revertToPlayer");
        waitState = new ControableEntityWait(this, stateMachine, stateData, "wait");

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void SetMovement(bool bool_)
    {
        thirdPersonController.canMove = bool_;
    }



    #region Destructables
    public void CheckDestructables()
    {
        TryDestruct();
    }
    public void TryDestruct()
    {
        Collider[] destructables = Physics.OverlapSphere(checkDestructablePos.transform.position, .5f, entityData.whatIsDestructable);
        //AudioManagerCS.instance.Play("playerHitSwing");

        if (destructables.Length >= 1)
        {
            foreach (Collider destructable in destructables)
            {
                IDamageable enemyHp = destructable.GetComponent<IDamageable>();
                if (enemyHp != null)
                {
                    enemyHp.Takedamage(1);
                }

            }
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkDestructablePos.transform.position, entityData.checkDestructableRadius);
    }



}
