using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttackData_EnemyMelee
{
    public string attackName;
    public int attackDamage;
    public float attackRange;
    public float moveSpeed;
    public float attackIndex;

    [Range(1f, 2f)]
    public float animationSpeed;
    public AttackTypeMelee attackType;
}

public enum AttackTypeMelee
{
    Close,
    Charge
}

public enum EnemyMelee_Type
{
    Regular,
    Shield,
    Dodge,
    AxeThrow
}

public enum Enemy_MeleeWeaponType
{
    OneHand,
    Throw,
    Box
}


public class EnemyMelee : Enemy
{
    #region State Melee Enemy
    public IdleStateMelee idleState { get; private set; }
    public MoveStateMelee moveState { get; private set; }
    public RecoveryStateMelee recoveryState { get; private set; }
    public RunStateMelee runState { get; private set; }
    public AttackStateMelee attackState { get; private set; }
    public DeadStateMelee deadState { get; private set; }
    public AbilityStateMelee abilityState { get; private set; }
    #endregion

    public void EnableMeleeAttackCheck(bool enable) => isMeleeAttackReady = enable;
    protected bool isMeleeAttackReady;


    [Header("Enemy Settings")]
    public EnemyMelee_Type meleeType;
    public Enemy_MeleeWeaponType weaponType;

    [Header("Shield")] //lá chắn
    public int shieldDurability;
    public Transform shieldTransform;

    [Header("Dodge")] //né tránh, nhào lộn
    public float dodgeCooldown;
    public float lastTimeDodge = -10f;

    [Header("Axe throw ability")] //khả năng ném rìu, ném kiếm,...
    public int axeDamage;
    public GameObject axePrefab;
    public float axeFlySpeed;
    public float axeAnimTimer;
    public float axeThrowCooldown;
    private float lastTimeAxeThrown;
    public Transform axeStartPoint;


    [Header("AttackData Data")]
    public AttackData_EnemyMelee attackData;
    public List<AttackData_EnemyMelee> attackList;
    private bool isAttackReady;



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
        this.deadState = new DeadStateMelee(this, stateMachine, "Idle"); // Idle anim is just a place holder,we use ragdoll
        this.abilityState = new AbilityStateMelee(this, stateMachine, "AxeThrow"); // Idle anim is just a place holder,we use ragdoll

    }

    protected override void Start()
    {
        base.Start();

        this.stateMachine.Initialize(this.idleState);

        this.ResetCooldown();
        this.InitializePerk();

    }

    protected override void Update()
    {
        base.Update();

        this.stateMachine.currentState.Update();
    }

    /*public void UpdateAttackData()
    {
        currentWeapon = visuals.currentWeaponModel.GetComponent<Enemy_WeaponModel>();

        if (currentWeapon.weaponData != null)
        {
            attackList = new List<AttackData_EnemyMelee>(currentWeapon.weaponData.attackData);
            turnSpeed = currentWeapon.weaponData.turnSpeed;
        }
    }*/

    protected override void InitializePerk()
    {
        if(this.meleeType == EnemyMelee_Type.AxeThrow)
        {
            this.weaponType = Enemy_MeleeWeaponType.Throw;
        }

        if (this.meleeType == EnemyMelee_Type.Shield)
        {
            this.anim.SetFloat("RunIndex", 1);
            this.shieldTransform.gameObject.SetActive(true);
            this.weaponType= Enemy_MeleeWeaponType.OneHand;
        }

        if (this.meleeType == EnemyMelee_Type.Dodge)
        {
            this.weaponType = Enemy_MeleeWeaponType.Box;
        }
    }

    public override void EnterBattleMode()
    {
        if (this.inBattleMode)
            return;

        base.EnterBattleMode();

        this.stateMachine.ChangeState(this.recoveryState);
    }

    #region Throw Axe
    public bool CanThrowAxe()
    {
        if(this.meleeType !=EnemyMelee_Type.AxeThrow) return false;

        if(Time.time >this.axeThrowCooldown + this.lastTimeAxeThrown)
        {
            this.lastTimeAxeThrown=Time.time;
            return true;
        }

        return false;
    }

    public void ThrowAxe()
    {
        Transform newAxe = WeaponSpawner.Instance.Spawn(WeaponSpawner.Axe_Weapon, this.axeStartPoint.position, this.axeStartPoint.rotation);
        newAxe.GetComponent<AxeWeapon>().AxeSetup(this.axeFlySpeed, this.player, this.axeAnimTimer, this.axeDamage);
        newAxe.gameObject.SetActive(true);

    }
    #endregion


    private void ResetCooldown()
    {
        this.lastTimeDodge -= this.dodgeCooldown;
        this.lastTimeAxeThrown -= this.axeThrowCooldown;
    }

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, this.player.position) < this.attackData.attackRange;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.attackData.attackRange);
    }

}
