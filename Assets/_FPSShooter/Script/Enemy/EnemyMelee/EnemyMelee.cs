using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[Serializable]
public struct AttackData
{
    public string attackName;
    public float attackRange;
    public float moveSpeed;
    public float attackIndex;

    [Range(1f,2f)]
    public float animationSpeed;
    public AttackTypeMelee attackType;
}

public enum AttackTypeMelee
{
    Close,Charge
}

public class EnemyMelee : Enemy
{
    #region State Melee Enemy
    public IdleStateMelee idleState { get; private set; }

    public MoveStateMelee moveState { get; private set; }
    public RecoveryStateMelee recoveryState { get; private set; }
    public RunStateMelee runState { get; private set; }
    public AttackStateMelee attackState { get; private set; }
    #endregion

    [Header("AttackData Data")]
    public AttackData attackData;
    public List<AttackData> attackList;


    [SerializeField] private Transform hiddenWeapon;
    [SerializeField] private Transform pulledWeapon;


    protected override void Awake()
    {
        base.Awake();

        this.idleState = new IdleStateMelee(this, stateMachine, "Idle");
        this.moveState = new MoveStateMelee(this, stateMachine, "Move");
        this.runState = new RunStateMelee(this, stateMachine, "Run");
        this.recoveryState = new RecoveryStateMelee(this, stateMachine, "Recovery");
        this.attackState = new AttackStateMelee(this, stateMachine, "Attack");

    }

    protected override void Start()
    {
        base.Start();

        this.stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        this.stateMachine.currentState.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.attackData.attackRange);
    }

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, this.player.position) < this.attackData.attackRange;


    public void PullWeapon()
    {
        hiddenWeapon.gameObject.SetActive(false);
        pulledWeapon.gameObject.SetActive(true);
    }
}
