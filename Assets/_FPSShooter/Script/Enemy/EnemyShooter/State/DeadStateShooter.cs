
public class DeadStateShooter : EnemyState
{
    private EnemyShooter enemy;

    public DeadStateShooter(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyShooter;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();


    }

    public override void Exit()
    {
        base.Exit();

    }
}
