using UnityEngine;

public class BulletCtrl : ResetMonoBehaviour
{
    [SerializeField] protected Collider cd;
    public Collider Cd=> cd;

    [SerializeField] protected Rigidbody rb;
    public Rigidbody Rb => rb;

    [SerializeField] protected TrailRenderer trailRenderer;
    public TrailRenderer TrailRenderer => trailRenderer;

    [SerializeField] protected Bullet bullet;
    public Bullet Bullet => bullet;

    [SerializeField] protected BulletDespawnByDistance bulletDespawnByDistance;
    public BulletDespawnByDistance BulletDespawnByDistance => bulletDespawnByDistance;

    public virtual void BulletSetp()
    {

    }


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadCollider();
        this.LoadRigidbody();
        this.LoadBullet();
        this.LoadTrailRenderer();
        this.LoadBulletDespawnByDistance();
    }

    protected virtual void LoadCollider()
    {
        if (this.cd != null) return;

        this.cd = GetComponent<Collider>();
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;

        this.rb = GetComponent<Rigidbody>();
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadBullet()
    {
        if (this.bullet != null) return;

        this.bullet = GetComponent<Bullet>();
        Debug.LogWarning(transform.name + ": LoadBullet", gameObject);
    }

    protected virtual void LoadTrailRenderer()
    {
        if (this.trailRenderer != null) return;

        this.trailRenderer = GetComponent<TrailRenderer>();
        Debug.LogWarning(transform.name + ": LoadTrailRenderer", gameObject);
    }
    protected virtual void LoadBulletDespawnByDistance()
    {
        if (this.bulletDespawnByDistance != null) return;

        this.bulletDespawnByDistance = GetComponentInChildren<BulletDespawnByDistance>();
        Debug.LogWarning(transform.name + ": LoadBulletDespawnByDistance", gameObject);
    }
    #endregion


}
