

public class EnemyMelee : Enemy
{
    #region State Melee Enemy
    public IdleStateMelee idleState {  get; private set; }

    public MoveStateMelee moveState {  get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        this.idleState = new IdleStateMelee(this, stateMachine, "Idle");
        this.moveState = new MoveStateMelee(this, stateMachine, "Move");

    }

    protected override void Start()
    {
        base.Start();

        this.stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        this.stateMachine.currentState.Update();
    }
}
