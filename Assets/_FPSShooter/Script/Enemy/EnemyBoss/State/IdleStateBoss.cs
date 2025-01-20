using UnityEngine;

public class IdleStateBoss : EnemyState
{
    private EnemyBoss enemy;

    public IdleStateBoss(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyBoss;
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
