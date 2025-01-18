using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCtrl : ResetMonoBehaviour
{
    public EnemyStateMachine enemyStateMachine { get; private set; }

    public EnemyType enemyType;
    public LayerMask whatIsAlly;
    public LayerMask whatIsPlayer;

    public NavMeshAgent Agent => agent;
    [SerializeField] protected NavMeshAgent agent;

    public Animator Anim => anim;
    [SerializeField] protected Animator anim;

    public Transform Player => player;
    [SerializeField] protected Transform player;

    public EnemyAttack EnemyAttack => enemyAttack;
    [SerializeField] protected EnemyAttack enemyAttack;

    public EnemyVisuals EnemyVisuals => enemyVisuals;
    [SerializeField] protected EnemyVisuals enemyVisuals;

    public EnemyDamageReceiver EnemyDamageReceiver => enemyDamageReceiver;
    [SerializeField] protected EnemyDamageReceiver enemyDamageReceiver;


    protected override void Awake()
    {
        base.Awake();

        this.enemyStateMachine = new EnemyStateMachine();
    }

    protected virtual void InitializePerk()
    {

    }

    #region Animation events

    public void AnimationTrigger() => this.enemyStateMachine.currentState.AnimationTrigger();

    public virtual void AbilityTrigger()
    {
        this.enemyStateMachine.currentState.AbilityTrigger();
    }
    #endregion


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWhatIsAlly();
        this.LoadWhatIsPlayer();
        this.LoadAnimator();
        this.LoadNavMeshAgent();
        this.LoadEnemyAttack();
        this.LoadEnemyVisuals();
        this.LoadPlayer();
        this.LoadEnemyDamageReceiver();
    }

    protected virtual void LoadWhatIsAlly()
    {
        if (this.whatIsAlly != 0) return;

        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        whatIsAlly = 1 << enemyLayerIndex;
        Debug.LogWarning(transform.name + ": LoadWhatIsAlly", gameObject);
    }

    protected virtual void LoadWhatIsPlayer()
    {
        if (this.whatIsPlayer != 0) return;

        int playerLayerIndex = LayerMask.NameToLayer("Player");
        whatIsPlayer = 1 << playerLayerIndex;
        Debug.LogWarning(transform.name + ": LoadWhatIsPlayer", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;

        this.anim = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;

        this.agent = GetComponent<NavMeshAgent>();
        Debug.LogWarning(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadEnemyAttack()
    {
        if (this.enemyAttack != null) return;

        this.enemyAttack = GetComponentInChildren<EnemyAttack>();
        Debug.LogWarning(transform.name + ": LoadEnemyAttack", gameObject);
    }

    protected virtual void LoadEnemyVisuals()
    {
        if (this.enemyVisuals != null) return;

        this.enemyVisuals = GetComponentInChildren<EnemyVisuals>();
        Debug.LogWarning(transform.name + ": LoadEnemyVisuals", gameObject);
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;

        this.player = GameObject.Find("Player").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void LoadEnemyDamageReceiver()
    {
        if (this.enemyDamageReceiver != null) return;

        this.enemyDamageReceiver = GetComponentInChildren<EnemyDamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadEnemyDamageReceiver", gameObject);
    }
    #endregion


}
