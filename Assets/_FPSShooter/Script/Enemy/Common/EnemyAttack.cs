using UnityEngine;
using UnityEngine.AI;


public class EnemyAttack : EnemyAbstract
{
    [Header("Idle data")]
    [SerializeField] protected float idleTime;
    public float IdleTime => idleTime;

    [SerializeField] protected float aggresionRange;
    public float AggresionRange => aggresionRange;

    private bool inBattleMode;
    public bool InBattleMode => inBattleMode;



    protected virtual void Update()
    {
        if (this.ShouldEnterBattleMode())
            this.EnterBattleMode();
    }

    protected bool ShouldEnterBattleMode()
    {
        if (IsPlayerInAgrresionRange() && !this.inBattleMode)
        {
            this.EnterBattleMode();
            return true;
        }

        return false;
    }

    public virtual void EnterBattleMode()
    {
        this.inBattleMode = true;
    }

    public bool IsPlayerInAgrresionRange() => Vector3.Distance(transform.position, this.enemyCtrl.Player.position) < this.aggresionRange;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position,this.aggresionRange);
    }

}
