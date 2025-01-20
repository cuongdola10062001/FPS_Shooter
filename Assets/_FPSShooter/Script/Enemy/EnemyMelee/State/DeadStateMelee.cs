
using UnityEngine;

public class DeadStateMelee : EnemyState
{
    private EnemyMelee enemy;

    public DeadStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
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