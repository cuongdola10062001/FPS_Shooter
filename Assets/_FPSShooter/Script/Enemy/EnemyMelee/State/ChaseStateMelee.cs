
using UnityEngine;

public class ChaseStateMelee : EnemyState
{
    private EnemyMelee enemy;

    public ChaseStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
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