
using UnityEngine;

public class IdleStateMelee : EnemyState
{
    private EnemyMelee enemy;

    public IdleStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.stateTimer = this.enemyBase.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (this.enemy.IsPlayerInAggresionRange())
        {
            this.stateMachine.ChangeState(this.enemy.recoveryState);

            return;
        }

        if (this.stateTimer < 0)
            this.stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();

    }
}