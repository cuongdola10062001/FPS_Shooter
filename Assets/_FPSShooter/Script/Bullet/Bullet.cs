using UnityEngine;

public class Bullet : ResetMonoBehaviour
{
    [SerializeField] protected BulletCtrl bulletCtrl;

    protected int bulletDamage;
    protected float disLimit;
    public float DistanceLimit => disLimit;

    protected Vector3 direction;
    public Vector3 Direction => direction;

    public virtual void SetupBullet(int bulletDamage, Vector3 direction, float disLimit)
    {
        this.bulletDamage = bulletDamage;
        this.direction = direction;
        this.disLimit = disLimit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletSpawner.Instance.Despawn(transform);

        Transform bulletImpactFX = FXSpawner.Instance.Spawn(FXSpawner.BulletVFX, transform.position, Quaternion.identity);
        bulletImpactFX.gameObject.SetActive(true);
    }


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;

        this.bulletCtrl = GetComponent<BulletCtrl>();
        Debug.LogWarning(transform.name + ": LoadBulletCtrl", gameObject);
    }
    #endregion
}
