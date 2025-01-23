
using UnityEngine;

public class DeadStateMelee : EnemyState
{
    private EnemyMelee enemy;
    private bool interactionDisabled;

    public DeadStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.interactionDisabled = false;

        this.stateTimer = 1.5f;
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