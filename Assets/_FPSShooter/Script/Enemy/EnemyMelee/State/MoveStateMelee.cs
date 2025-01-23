
using UnityEngine;

public class MoveStateMelee : EnemyState
{
    private EnemyMelee enemy;
    private Vector3 destination;

    public MoveStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.enemy.agent.speed = enemy.walkSpeed;

        this.destination = this.enemy.player.transform.position;
        this.enemy.agent.SetDestination(destination);
    }

    public override void Update()
    {
        base.Update();

        /*if (this.enemy.IsPlayerInAggresionRange())
        {
            this.stateMachine.ChangeState(this.enemy.recoveryState);

            return;
        }*/

        this.enemy.FaceTarget(this.destination);

        if (this.enemy.agent.remainingDistance <= this.enemy.agent.stoppingDistance + .05f)
            this.stateMachine.ChangeState(this.enemy.idleState);
    }
}