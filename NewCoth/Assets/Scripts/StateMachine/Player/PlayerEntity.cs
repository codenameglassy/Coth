using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using DG.Tweening;
using Cinemachine;
public class PlayerEntity : MonoBehaviour
{
    //Components
    public Animator anim { get; private set; }
    public PlayerFiniteStateMachine stateMachine { get; private set; }

    //States
    #region Player States
    public PlayerIdleState idleState { get; private set; }
    public PlayerDodgeForwardState dodgeForwardState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerPrayState prayState { get; private set; }
    public PlayerIdleState_Staff idleState_Staff { get; private set; }
    public PlayerSheatheStaffState sheatheStaffState { get; private set; }
    public PlayerSheatheUnarmState sheatheUnarmState { get; private set; }
    public PlayerElementalnfusionState elementalFusionState { get; private set; }
    public PlayerStaffInteractState staffInteractState { get; private set; }
    public PlayerCastControlState castControlState { get; private set; }
    public PlayerWakeUpState wakeUpState { get; private set; }
    public PlayerWaitState waitState { get; private set; }
    public PlayerSleepState sleepState { get; private set; }
    public PlayerRangeAttackState rangeAttackState { get; private set; }
    public PlayerGlideState glideState { get; private set; }
    public PlayerSkatingState skatingState { get; private set; }
    public PlayerPushingState pushingState { get; private set; }
    #endregion

    [Header("Components")]
    [SerializeField] private PlayerStateData stateData;
    public PlayerEntityData entityData;
    [SerializeField] private ThirdPersonController thirdPersonController;

    [Header("Check Surroundings")]
    public Transform checkEnemyPos;

    [Header("Animation Overrides")]
    public RuntimeAnimatorController staffAnimationController;
    public RuntimeAnimatorController unArmAnimationController;

    [Header("Attack")]
    public Transform attackPos;
    public Transform rangeAttackPos;

    [Header("Pray")]
    public Transform prayVfxSpawnPos;

    [Header("Weapons")]
    public GameObject staff;

    [Header("Elemental")]
    public GameObject water;

   

    private void Awake()
    {
        anim = GetComponent<Animator>();
        stateMachine = new PlayerFiniteStateMachine();


        idleState = new PlayerIdleState(this, stateMachine, stateData, "idle");
        dodgeForwardState = new PlayerDodgeForwardState(this, stateMachine, stateData, "dodge");
        attackState = new PlayerAttackState(this, stateMachine, stateData, "attack");
        prayState = new PlayerPrayState(this, stateMachine, stateData, "pray");
        idleState_Staff = new PlayerIdleState_Staff(this, stateMachine, stateData, "idle");
        sheatheStaffState = new PlayerSheatheStaffState(this, stateMachine, stateData, "sheathe");
        sheatheUnarmState = new PlayerSheatheUnarmState(this, stateMachine, stateData, "sheathe");
        elementalFusionState = new PlayerElementalnfusionState(this, stateMachine, stateData, "infuse");
        staffInteractState = new PlayerStaffInteractState(this, stateMachine, stateData, "interactStaff");
        castControlState = new PlayerCastControlState(this, stateMachine, stateData, "castControl");
        wakeUpState = new PlayerWakeUpState(this, stateMachine, stateData, "wakeUp");
        waitState = new PlayerWaitState(this, stateMachine, stateData, "wait");
        sleepState = new PlayerSleepState(this, stateMachine, stateData, "sleep");
        rangeAttackState = new PlayerRangeAttackState(this, stateMachine, stateData, "rangeAttack");
        glideState = new PlayerGlideState(this, stateMachine, stateData, "glide");
        skatingState = new PlayerSkatingState(this, stateMachine, stateData, "skate");
        pushingState = new PlayerPushingState(this, stateMachine, stateData, "push");

        stateMachine.Initialize(idleState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

    
    #region Dodge
    public void DodgeForward()
    {
        thirdPersonController.DodgeForward();
    }

    public void SetDodge(bool bool_)
    {
        thirdPersonController.SetDodgeing(bool_);
    }
    #endregion

    #region FaceThis, GetClose

    public void FaceEnemy()
    {
        Transform target = FindClosestTarget();
        FaceThis(target.position);
    }

    public void FaceThis(Vector3 target)
    {
        Vector3 target_ = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;
        transform.DOLocalRotateQuaternion(lookAtRotation, 0.2f);
    }

    public Vector3 TargetOffset(Vector3 target, float deltaDistance)
    {
        Vector3 position;
        position = target;
        return Vector3.MoveTowards(position, transform.position, deltaDistance);
    }

    public void GetClose() // Animation Event ---- for Moving Close to Target
    {
        Transform target = FindClosestTarget();
        FaceThis(target.position);
        Vector3 finalPos = TargetOffset(target.position, entityData.attackDeltaDistance);
        finalPos.y = 0;
        transform.DOMove(finalPos, 0.2f);
    }


    #endregion

    #region Check Surroundings
    public void TryDestruct()
    {
        Collider[] destructables = Physics.OverlapSphere(attackPos.position, .5f, entityData.whatIsEnemy);
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
    public bool CheckEnemyInRange()
    {
        bool isEnemyInRange = Physics.CheckSphere(checkEnemyPos.position, entityData.checkEnemyRange, entityData.whatIsEnemy);

        return isEnemyInRange;
    }

    public void AlterInteract()
    {
        Collider[] altars = Physics.OverlapSphere(attackPos.position, entityData.interactRange, entityData.whatIsPrayInteract);

        if (altars.Length >= 1)
        {
            foreach (Collider altar in altars)
            {
               
                IInteractable interact = altar.GetComponent<IInteractable>();
                if (interact != null)
                {
                    interact.Interact(gameObject);
                   
                }

            }
        }
    }

    public void CheckLoots()
    {
        Collider[] loots = Physics.OverlapSphere(transform.position, entityData.lootRange, entityData.whatIsLootable);

        if(loots.Length >= 1)
        {
            foreach (Collider item in loots)
            {
                ILoot loot = item.GetComponent<ILoot>();
                if(loot != null)
                {
                    loot.Loot(gameObject);
                }
            }
        }
    }

    #endregion

    #region Attack

    public void TryAttack()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPos.position, entityData.attackRange, entityData.whatIsEnemy);
        AudioManagerCS.instance.Play("playerHitSwing");

        if (enemies.Length >= 1)
        {
            foreach (Collider enemy in enemies)
            {
                IDamageable enemyHp = enemy.GetComponent<IDamageable>();
                if (enemyHp != null)
                {
                    enemyHp.Takedamage(10);
                }

            }
        }
    }

    public void RangeAttack()
    {
        //Quaternion rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        Instantiate(entityData.rangeAttackVfxPrefab, rangeAttackPos.position, transform.rotation);
    }
    #endregion

    #region Handle Pray Input

    public void HandlePrayInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(prayState);
        }
    }

   
    public void SpawnPrayVfxRoutine()
    {
        StartCoroutine(Enum_SpawnPrayVfx());
    }

    IEnumerator Enum_SpawnPrayVfx()
    {
        

        yield return new WaitForSeconds(1f);
        SpawnPrayVfx();
        AlterInteract();
        yield return new WaitForSeconds(1f);
       

        yield return new WaitForSeconds(.5f);
       
        Instantiate(entityData.prayPulseVfxPrefab, prayVfxSpawnPos.position, entityData.prayvfxRotaion);
       
    }

    public void SpawnPrayVfx()
    {
        Instantiate(entityData.prayVfxPrefab, prayVfxSpawnPos.position, entityData.prayvfxRotaion);
    }
    public void SpawnCastControlVfx()
    {
        Instantiate(entityData.castControlVfx, prayVfxSpawnPos.position, entityData.prayvfxRotaion);
    }

    #endregion

    #region Staff

    public void InputSwitchToStaff()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(sheatheStaffState);
        }
    }

    public void InputSwitchToUnarm()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(sheatheUnarmState);
        }
    }

    public void SheatheStaff()
    {
        stateMachine.ChangeState(idleState_Staff);
        OverrideAnimationController(staffAnimationController);
        staff.SetActive(true);
    }

    public void SheatheUnarm()
    {
        staff.SetActive(false);
        Unfuse();
        stateMachine.ChangeState(idleState);
        OverrideAnimationController(unArmAnimationController);
    }
    public void HandleStaffInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(staffInteractState);
        }
    }
    #endregion

    #region Elemental Infusion
    public void InfuseWater()
    {
        stateMachine.ChangeState(elementalFusionState);
        StartCoroutine(Enum_InfuseWater());
    }

    public void Unfuse()
    {
        water.SetActive(false);
    }

    IEnumerator Enum_InfuseWater()
    {
        yield return new WaitForSeconds(stateData.infuseDelay);
        water.SetActive(true);
    }
    #endregion

    #region Glide
    public void HandleGlide()
    {
       thirdPersonController.HandleGlide();
    }

    public void CheckSwitchToGlideState()
    {
        if(!thirdPersonController.IsGrounded() && Input.GetKeyDown(KeyCode.Space) && thirdPersonController.JumpTimeoutDelta() >= 0.2f)
        {
            stateMachine.ChangeState(glideState);
        }
    }

   public void CheckGroundWhileGliding()
    {
        if (thirdPersonController.IsGrounded())
        {
            Debug.Log("Touching ground, Glidding off");
            stateMachine.ChangeState(idleState);
        }
    }

    #endregion

    #region Propelled Jump

    public bool CheckIfPropelledJump()
    {
        bool isTouchingPropelledJumpArea = Physics.CheckSphere(transform.position, entityData.propelledJumpCheckRadius, entityData.whatIsPropelledJumpArea);
        return isTouchingPropelledJumpArea;
    }

    #endregion

    #region Chant
    public void Chant()
    {
        Instantiate(entityData.chantVfxPrefab, transform.position, Quaternion.identity);
    }
    #endregion

    #region Push
    public GameObject pushGo;
    public Transform pushPos;
    public void TryPush()
    {
        transform.DOMove(pushPos.position, 1f);
    }
    #endregion

    public void InitializeIdle()
    {
        stateMachine.ChangeState(idleState);
    }
   
    public void OverrideAnimationController(RuntimeAnimatorController newController)
    {
        if (anim == null)
        {
            Debug.LogWarning("Animator component is not assigned.");
            return;
        }

        if (newController == null)
        {
            Debug.LogWarning("Provided RuntimeAnimatorController is null.");
            return;
        }

        anim.runtimeAnimatorController = newController;
        Debug.Log("Animation controller successfully overridden.");
    }

    Transform FindClosestTarget()
    {
        if (EnemySpawnManager.instance.activeEnemyInScene == null || EnemySpawnManager.instance.activeEnemyInScene.Count == 0)
            return null;

        Transform closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (Transform target in EnemySpawnManager.instance.activeEnemyInScene)
        {
            if (target == null) continue;

            Vector3 directionToTarget = target.position - playerPosition;
            float distanceSqr = directionToTarget.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closest = target;
            }
        }

        return closest;
    }

    public ThirdPersonController GetThirdPersonController()
    {
        return thirdPersonController;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(checkEnemyPos.position, entityData.checkEnemyRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, entityData.attackRange);
    }
}
