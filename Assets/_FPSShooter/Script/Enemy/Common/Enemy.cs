using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { Melee,Shooter,Boss,Random}

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : ResetMonoBehaviour
{
    public EnemyType enemyType;
    public LayerMask whatIsAlly;
    public LayerMask whatIsPlayer;

    [Header("Idle data")]
    public float idleTime;
    public float aggresionRange;

    [Header("Move data")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3;
    public float turnSpeed;
    public bool ManualMovement => this.manualMovement;
    private bool manualMovement;
    public bool ManualRotation => this.manualRotation;
    private bool manualRotation;

    [SerializeField] private Transform[] patrolPoints;
    private Vector3[] patrolPointsPosition;
    private int currentPatrolIndex;

    public bool inBattleMode {  get; private set; }
    protected bool isMeleeAttackReady;


    public Transform player { get; private set; }

    #region Components
    public EnemyStateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public NavMeshAgent agent { get; private set; }


    #endregion


    protected override void Awake()
    {
        base.Awake();

        this.stateMachine = new EnemyStateMachine();

        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    protected override void Start()
    {
        base.Start();

        this.InitializePatrolPoints();
    }

    protected virtual void Update()
    {
        if (this.ShouldEnterBattleMode())
            this.EnterBattleMode();
    }

   
    protected bool ShouldEnterBattleMode()
    {
        if(this.IsPlayerInAggresionRange() && !this.inBattleMode)
        {
            this.EnterBattleMode();
            return true;
        }

        return false;
    }

    public virtual void EnterBattleMode()
    {
        inBattleMode = true;
    }

    public void EnableMeleeAttackCheck(bool enable) => isMeleeAttackReady = enable;



    public void FaceTarget(Vector3 target, float turnSpeed = 0)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        Vector3 currentEulerAngels = transform.rotation.eulerAngles;

        if (turnSpeed == 0)
            turnSpeed = this.turnSpeed;

        float yRotation = Mathf.LerpAngle(currentEulerAngels.y, targetRotation.eulerAngles.y, turnSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentEulerAngels.x, yRotation, currentEulerAngels.z);
    }


    #region Animation events
    public void ActivateManualMovement(bool manualMovement) => this.manualMovement = manualMovement;
    public void ActivateManualRotation(bool manualRotation) => this.manualRotation = manualRotation;

    public void AnimationTrigger() => this.stateMachine.currentState.AnimationTrigger();

    public virtual void AbilityTrigger()
    {
        this.stateMachine.currentState.AbilityTrigger();
    }

    #endregion
    
    #region Patrol logic
    public Vector3 GetPatrolDestination()
    {
        Vector3 destination = patrolPoints[currentPatrolIndex].transform.position;

        currentPatrolIndex++;

        if (currentPatrolIndex >= patrolPoints.Length)
            currentPatrolIndex = 0;

        return destination;
    }

    private void InitializePatrolPoints()
    {
        patrolPointsPosition = new Vector3[patrolPoints.Length];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPointsPosition[i] = patrolPoints[i].position;
            patrolPoints[i].gameObject.SetActive(false);
        }
    }
    #endregion

   
    public bool IsPlayerInAggresionRange() => Vector3.Distance(transform.position, this.player.position) < this.aggresionRange;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, this.aggresionRange);
    }
}
