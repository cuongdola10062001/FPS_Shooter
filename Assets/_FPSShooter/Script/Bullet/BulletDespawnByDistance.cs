using UnityEngine;

public class BulletDespawnByDistance : DespawnByDistance
{
    [SerializeField] protected BulletCtrl bulletCtrl;

    protected override void ResetValueWhenOnEnable()
    {
        base.ResetValueWhenOnEnable();

        this.disLimit = this.bulletCtrl.Bullet.DistanceLimit;
    }

    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;

        this.bulletCtrl = GetComponentInParent<BulletCtrl>();
        Debug.LogWarning(transform.name + ": LoadBulletCtrl", gameObject);
    }
}
