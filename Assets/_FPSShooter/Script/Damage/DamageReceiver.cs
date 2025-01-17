using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class DamageReceiver : ResetMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected Collider cd;
    [SerializeField] protected int hp = 1;
    [SerializeField] protected int hpMax = 10;
    [SerializeField] protected bool isDead = false;
    public virtual bool IsDead()=>this.isDead;
    

    public int HP=>this.hp;
    public int HPMax => this.hpMax;

    protected override void OnEnable()
    {
        base.OnEnable();

        this.Reborn();
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        this.Reborn();
    }


    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }

    public virtual void Add(int add)
    {
        if (this.isDead) return;

        this.hp+= add;
        if (this.hp > this.hpMax) this.hp = this.hpMax;
    }

    public virtual void Deduct(int deduct)
    {
        if(this.isDead) return;

        this.hp-= deduct;
        if (this.hp < 0) this.hp = 0;

        this.CheckIsDead();
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;

        this.isDead=true;
        this.OnDead();
    }

    

    protected abstract void OnDead();


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this.cd != null) return;
        this.cd = GetComponent<Collider>();
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    #endregion
}
