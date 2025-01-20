
using UnityEngine;

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