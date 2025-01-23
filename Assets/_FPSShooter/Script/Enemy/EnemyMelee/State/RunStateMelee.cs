
using UnityEngine;

public class RunStateMelee : EnemyState
{
    private EnemyMelee enemy;
    private float lastTimeUpdatedDistanation;

    public RunStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.enemy.agent.speed = this.enemy.runSpeed;
        this.enemy.agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (this.enemy.PlayerInAttackRange())
        {
            this.stateMachine.ChangeState(this.enemy.attackState);
        }

        this.enemy.FaceTarget(this.enemy.player.transform.position);

        if (this.CanUpdateDestination())
        {
            this.enemy.agent.destination = this.enemy.player.transform.position;
        }
    }

    private bool CanUpdateDestination()
    {
        if(Time.time> this.lastTimeUpdatedDistanation + .25f)
        {
            lastTimeUpdatedDistanation=Time.time;

            return true;
        }

        return false;
    }
}