using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { Melee, Shooter, Boss, Random }

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

    public bool inBattleMode;

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
    }

    protected virtual void Update()
    {
        if (this.ShouldEnterBattleMode())
            this.EnterBattleMode();
    }

    protected virtual void InitializePerk()
    {

    }


    protected bool ShouldEnterBattleMode()
    {
        if (this.IsPlayerInAggresionRange() && !this.inBattleMode)
        {
            this.EnterBattleMode();
            return true;
        }

        return false;
    }

    public virtual void EnterBattleMode()
    {
        this.inBattleMode = true;
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


    #region Animation events
    public void ActivateManualMovement(bool manualMovement) => this.manualMovement = manualMovement;
    public void ActivateManualRotation(bool manualRotation) => this.manualRotation = manualRotation;

    public void AnimationTrigger() => this.stateMachine.currentState.AnimationTrigger();

    public virtual void AbilityTrigger()
    {
        this.stateMachine.currentState.AbilityTrigger();
    }

    #endregion

    public bool IsPlayerInAggresionRange() => Vector3.Distance(transform.position, this.player.position) < this.aggresionRange;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, this.aggresionRange);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWhatIsAlly();
        this.LoadWhatIsPlayer();
        this.LoadAgent();
        this.LoadAnimator();
        this.LoadPlayer();
    }

    protected virtual void LoadWhatIsAlly()
    {
        if (this.whatIsAlly.value != 0) return;

        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        whatIsAlly = 1 << enemyLayerIndex;

        Debug.LogWarning(transform.name + ": LoadWhatIsAlly", gameObject);
    }

    protected virtual void LoadWhatIsPlayer()
    {
        if (this.whatIsPlayer.value != 0) return;

        int playerLayerIndex = LayerMask.NameToLayer("Player");
        this.whatIsPlayer = 1 << playerLayerIndex;

        Debug.LogWarning(transform.name + ": LoadWhatIsPlayer", gameObject);
    }

    protected virtual void LoadAgent()
    {
        if (this.agent!=null) return;

        this.agent = GetComponent<NavMeshAgent>();

        Debug.LogWarning(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;

        this.anim = GetComponentInChildren<Animator>();

        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;

        this.player = GameObject.Find("Player").transform;

        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }
    #endregion
}
