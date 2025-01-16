using UnityEngine;

public class BulletFly : ResetMonoBehaviour
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    [SerializeField] protected float speed = 200f;
    [SerializeField] protected Vector3 direction;

    protected override void OnEnable()
    {
        base.OnEnable();

        this.AppllyForceFlyBullet();
    }

    protected virtual void AppllyForceFlyBullet()
    {

        this.bulletCtrl.Rb.linearVelocity = this.direction * speed;
    }

    protected override void ResetValueWhenOnEnable()
    {
        base.ResetValueWhenOnEnable();

        this.direction = this.bulletCtrl.Bullet.Direction;
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

        this.bulletCtrl = GetComponentInParent<BulletCtrl>();
        Debug.LogWarning(transform.name + ": LoadBulletCtrl", gameObject);
    }
    #endregion
}
