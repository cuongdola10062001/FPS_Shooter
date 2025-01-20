using UnityEngine;

public class AbilityStateBoss : EnemyState
{
    private EnemyBoss enemy;

    public AbilityStateBoss(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
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
