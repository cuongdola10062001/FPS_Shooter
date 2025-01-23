
using UnityEngine;
using UnityEngine.AI;


public class EnemyState
{
	protected Enemy enemyBase;
	protected EnemyStateMachine stateMachine;

	protected string animBoolName;
	protected float stateTimer;

	protected bool triggerCalled;

	public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName)
	{
		this.enemyBase = enemyBase;
		this.stateMachine = enemyStateMachine;
		this.animBoolName = animBoolName;
	}

	public virtual void Enter()
	{
		this.enemyBase.anim.SetBool(this.animBoolName, true);

		this.triggerCalled = false;
	}

	public virtual void Update()
	{
		this.stateTimer -= Time.deltaTime;
	}

	public virtual void Exit()
	{
		this.enemyBase.anim.SetBool(this.animBoolName, false);
	}

	public void AnimationTrigger() => this.triggerCalled = true;

	public virtual void AbilityTrigger()
	{

	}
}