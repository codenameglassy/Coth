using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyEntity : MonoBehaviour
{
   
    public GameObject modelGO{ get; private set; }
    public NavMeshAgent navmeshAgent { get; private set; }
    public Animator anim { get; private set; }
    public EnemyFiniteStateMachine stateMachine { get; private set; }
    public Transform target { get; private set; }

    [Header("Components")]
    [SerializeField] private D_EnemyData enemyData;

    [Header("Patrol")]
    private List<Transform> patrolPos = new List<Transform>();
    private int currentPatrolIndex = 0;

    [Header("Check Surroundings")]
    [SerializeField] private Transform checkPlayerPos;

    [Header("Attack")]
    [SerializeField] private Transform attackPos;

    // Start is called before the first frame update
    public virtual void Start()
    {
        modelGO = transform.Find("Model").gameObject;
        navmeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerEntity>().transform; //set target

        patrolPos = EnemySpawnManager.instance.patrolPos;

        stateMachine = new EnemyFiniteStateMachine();
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

    #region Patrol

    public virtual void GoToNextPatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPos.Count;
        SetDestination(patrolPos[currentPatrolIndex]);
    }

    #endregion

    #region Navmesh-Agent

    public virtual void StopMovement()
    {
        navmeshAgent.ResetPath();  // Clear the path
        navmeshAgent.velocity = Vector3.zero;  // Stop any residual motion
    }

    public bool HasReachedDestination()
    {
        // Check if the agent has reached the current patrol point
        return !navmeshAgent.pathPending &&
               navmeshAgent.remainingDistance <= 0.5f &&
               !navmeshAgent.hasPath;
    }
    public virtual void SetDestination(Transform target_)
    {
        navmeshAgent.SetDestination(target_.position);
    }

    #endregion

    #region Dotween [FaceThis]

    public virtual void FaceThis(Vector3 target)
    {
        Vector3 target_ = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;
        transform.DOLocalRotateQuaternion(lookAtRotation, 0.2f);
    }

    #endregion

    #region Check Surrounding
    public virtual bool IsCheckPlayerInMinDistance()
    {
        bool isPlayerInMinRange = Physics.CheckSphere(checkPlayerPos.position, enemyData.checkPlayerInMinDistance, enemyData.whatIsPlayer);
        return isPlayerInMinRange;
    }

    public virtual bool IsCheckPlayerInMaxDistance()
    {
        bool isPlayerInMaxRange = Physics.CheckSphere(checkPlayerPos.position, enemyData.checkPlayerInMaxDistance, enemyData.whatIsPlayer);
        return isPlayerInMaxRange;
    }

    #endregion

    #region Attack

    public void TryAttack()
    {
        bool isPlayerInAttackRange = Physics.CheckSphere(attackPos.position, enemyData.attackRange, enemyData.whatIsPlayer);
        AudioManagerCS.instance.Play("enemyHitSwing");
        if (isPlayerInAttackRange)
        {
            target.GetComponent<IDamageable>().Takedamage(10);
        }
        else
        {
            Debug.Log("Missed");
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPlayerPos.position, enemyData.checkPlayerInMaxDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPlayerPos.position, enemyData.checkPlayerInMinDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, enemyData.attackRange);
    }
}
