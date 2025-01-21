using UnityEngine;
using UnityEngine.AI;

public class Enemy : ResetMonoBehaviour
{
    public EnemyStateMachine stateMachine { get; private set; }

    public Animator anim { get; private set; }
    public NavMeshAgent agent { get; private set; }

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

    public Transform player { get; private set; }


    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolIndex;


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


    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, this.aggresionRange);


    }
    public void ActivateManualMovement(bool manualMovement) => this.manualMovement = manualMovement;
    public void ActivateManualRotation(bool manualRotation) => this.manualRotation = manualRotation;

    public void AnimationTrigger() => this.stateMachine.currentState.AnimationTrigger();

    public bool PlayerInAggresionRange() => Vector3.Distance(transform.position, this.player.position) < this.aggresionRange;

    public Vector3 GetPatrolDestination()
    {
        Vector3 destination = patrolPoints[currentPatrolIndex].transform.position;

        currentPatrolIndex++;

        if (currentPatrolIndex >= patrolPoints.Length)
            currentPatrolIndex = 0;

        return destination;
    }

    public void FaceTarget(Vector3 target, float turnSpeed = 0)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        Vector3 currentEulerAngels = transform.rotation.eulerAngles;

        if (turnSpeed == 0)
            turnSpeed = this.turnSpeed;

        float yRotation = Mathf.LerpAngle(currentEulerAngels.y, targetRotation.eulerAngles.y, turnSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentEulerAngels.x, yRotation, currentEulerAngels.z);
    }
}
