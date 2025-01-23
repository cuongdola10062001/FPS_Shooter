public class RecoveryStateMelee : EnemyState
{
    private EnemyMelee enemy;

    public RecoveryStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.enemy.agent.isStopped = true;
    }

    public override void Update()
    {
        base.Update();

        this.enemy.FaceTarget(this.enemy.player.position);

        if (this.triggerCalled)
        {
            if (this.enemy.CanThrowAxe())
            {
                this.stateMachine.ChangeState(this.enemy.abilityState);
            }
            else if (this.enemy.PlayerInAttackRange())
            {
                this.stateMachine.ChangeState(this.enemy.attackState);
            }
            else
            {
                this.stateMachine.ChangeState(this.enemy.runState);
            }
        }

    }
}