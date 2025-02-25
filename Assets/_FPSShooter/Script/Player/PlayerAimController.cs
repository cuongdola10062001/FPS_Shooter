using UnityEngine;

public class PlayerAimController : PlayerAbstract
{
    [SerializeField] protected LineRenderer aimLaser;

    [Space]

    [SerializeField] protected LayerMask aimLayerMask;

    protected Vector2 mouseInput;
    protected RaycastHit lastKnownMouseHit;

    protected virtual void Update()
    {

        this.UpdateAimLaserVisuals();
    }

    //[SerializeField] protected float laserTipLenght;
    [SerializeField] protected float distanceLimitRate = 2.5f;
    protected virtual void UpdateAimLaserVisuals()
    {
        this.aimLaser.enabled = this.playerCtrl.PlayerWeaponController.WeaponReady;

        if (this.aimLaser.enabled == false)
            return;

        WeaponGunModel weaponModel = this.playerCtrl.PlayerWeaponController.CurrentWeapon;

        Transform gunPoint = weaponModel.gunPoint;
        Vector3 laserDirection = this.playerCtrl.PlayerAttack.BulletDirection();
        Vector3 endPoint = gunPoint.position + laserDirection * weaponModel.weaponData.shootingDistanceLimit / this.distanceLimitRate;


        aimLaser.SetPosition(0, gunPoint.position);
        aimLaser.SetPosition(1, endPoint);
        //aimLaser.SetPosition(2, endPoint + laserDirection * laserTipLenght);
    }



    public virtual void EnableAimLaser(bool enable) => this.aimLaser.enabled = enable;

    public virtual RaycastHit GetMouseHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(this.mouseInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, this.aimLayerMask))
        {
            this.lastKnownMouseHit = hitInfo;

            return hitInfo;
        }

        return lastKnownMouseHit;
    }

    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();

        this.controls.Character.Aim.performed += context => this.mouseInput = context.ReadValue<Vector2>();
        this.controls.Character.Aim.canceled += context => this.mouseInput = Vector2.zero;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAimLaser();
        this.LoadAimLayerMask();
    }

    protected virtual void LoadAimLaser()
    {
        if (this.aimLaser != null) return;

        this.aimLaser = GetComponentInChildren<LineRenderer>();
        Debug.LogWarning(transform.name + ": LoadAimLaser", gameObject);
    }

    protected virtual void LoadAimLayerMask()
    {
        if (this.aimLayerMask.value != 0) return;

        int playerLayer = LayerMask.NameToLayer("Player");

        this.aimLayerMask = ~(1 << playerLayer);
        Debug.LogWarning(transform.name + ": LoadAimLayerMask", gameObject);
    }
    #endregion
}
