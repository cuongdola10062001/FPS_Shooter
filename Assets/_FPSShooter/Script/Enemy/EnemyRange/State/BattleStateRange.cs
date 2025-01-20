
public class BattleStateRange : EnemyState
{
    private EnemyRange enemy;

    public BattleStateRange(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyRange;
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
