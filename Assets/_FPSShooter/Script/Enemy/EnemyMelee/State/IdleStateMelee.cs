
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

        this.stateTimer = this.enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (this.stateTimer < 0)
            this.stateMachine.ChangeState(this.enemy.moveState);
    }
}