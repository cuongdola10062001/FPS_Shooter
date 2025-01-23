
using UnityEngine;

public class AbilityStateMelee : EnemyState
{
    private EnemyMelee enemy;

    private Vector3 movementDirection;

    private const float MAX_MOVEMENT_DISTANCE = 20;

    private float moveSpeed;

    public AbilityStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.moveSpeed = this.enemy.walkSpeed;
        this.movementDirection=this.enemy.transform.position+(this.enemy.transform.forward*MAX_MOVEMENT_DISTANCE);
    }

    public override void Update()
    {
        base.Update();

        if (this.enemy.ManualRotation)
        {
            this.enemy.FaceTarget(this.enemy.player.position);
            this.movementDirection = this.enemy.transform.position + (this.enemy.transform.forward * MAX_MOVEMENT_DISTANCE);
        }

        if (this.enemy.ManualMovement)
        {
            this.enemy.transform.position= Vector3.MoveTowards(this.enemy.transform.position,this.movementDirection,this.enemy.walkSpeed*Time.deltaTime);
        }

        if(this.triggerCalled)
            this.stateMachine.ChangeState(this.enemy.recoveryState);
    }

    public override void Exit()
    {
        base.Exit();

        this.enemy.walkSpeed = this.moveSpeed;
        this.enemy.anim.SetFloat("RecoveryIndex", 0);
    }

    public override void AbilityTrigger()
    {
        base.AbilityTrigger();

        this.enemy.ThrowAxe();
    }
}