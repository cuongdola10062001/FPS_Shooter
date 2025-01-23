using System.Collections.Generic;
using UnityEngine;

public class AttackStateMelee : EnemyState
{
    private EnemyMelee enemy;

    private Vector3 attackDirection;

    private float attackMoveSpeed;

    private const float MAX_ATTACK_DISTANCE = 50f;

    public AttackStateMelee(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemyBase as EnemyMelee;
    }

    public override void Enter()
    {
        base.Enter();

        this.attackMoveSpeed = this.enemy.attackData.moveSpeed;
        this.enemy.anim.SetFloat("AttackAnimationSpeed", this.enemy.attackData.animationSpeed);
        this.enemy.anim.SetFloat("AttackIndex", this.enemy.attackData.attackIndex);
        this.enemy.anim.SetFloat("SlashAttackIndex", Random.Range(0, 6)); // we have 6 attacks with index from 0 to 5



        this.enemy.agent.isStopped = true;
        this.enemy.agent.velocity = Vector3.zero;

        this.attackDirection = this.enemy.transform.position + (this.enemy.transform.forward * MAX_ATTACK_DISTANCE);
    }

    public override void Update()
    {
        base.Update();

        if (this.enemy.ManualRotation)
        {
            this.enemy.FaceTarget(this.enemy.player.position);
            this.attackDirection = this.enemy.transform.position + (this.enemy.transform.forward * MAX_ATTACK_DISTANCE);
        }


        if (this.enemy.ManualMovement)
        {
            this.enemy.transform.position =
                Vector3.MoveTowards(this.enemy.transform.position, this.attackDirection, this.attackMoveSpeed * Time.deltaTime);
        }


        if (triggerCalled)
        {
            if (enemy.CanThrowAxe())
                this.stateMachine.ChangeState(this.enemy.abilityState);
            else if (this.enemy.PlayerInAttackRange())
                this.stateMachine.ChangeState(this.enemy.recoveryState);
            else
                this.stateMachine.ChangeState(this.enemy.runState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        this.SetupNextAttack();
    }

    private void SetupNextAttack()
    {
        int recoveryIndex = this.PlayerClose() ? 1 : 0;
        this.enemy.anim.SetFloat("RecoveryIndex", recoveryIndex);
        this.enemy.attackData = this.UpdateAttackData();
    }

    private bool PlayerClose() => Vector3.Distance(this.enemy.transform.position, enemy.player.position) <= 1;

    private AttackData_EnemyMelee UpdateAttackData()
    {
        List<AttackData_EnemyMelee> validAttacks = new List<AttackData_EnemyMelee>(this.enemy.attackList);

        if (this.PlayerClose())
            validAttacks.RemoveAll(parameter => parameter.attackType == AttackTypeMelee.Charge);

        int random = Random.Range(0, validAttacks.Count);

        return validAttacks[random];
    }
}