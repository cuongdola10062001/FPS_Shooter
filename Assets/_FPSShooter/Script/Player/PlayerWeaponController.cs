using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : PlayerAbstract
{
    private const float REFERENCE_BULLET_SPEED = 20;

    [SerializeField] protected List<WeaponData> defaultWeaponData;

    public Weapon CurrentWeapon => currentWeapon;
    [SerializeField] protected Weapon currentWeapon;

    public bool WeaponReady=> weaponReady;
    protected bool weaponReady;
    public bool IsShooting => isShooting;
    protected bool isShooting;

    [Header("Bullet details")]
    [SerializeField] protected float bulletImpactForce = 100;
    [SerializeField] protected float bulletSpeed;

    public void SetWeaponReady(bool ready)
    {
        this.weaponReady = ready;
    }
    public Transform GunPoint() => this.playerCtrl.PlayerWeaponVisuals.CurrentWeaponModel().gunPoint;


    protected virtual void Update()
    {
        if (this.isShooting)
        {
            this.Shoot();
        }
    }

    protected virtual void Shoot()
    {
        if (!this.weaponReady) return;

        if (!this.currentWeapon.CanShoot()) return;

        this.playerCtrl.PlayerWeaponVisuals.PlayFireAnimation();

        if (this.currentWeapon.shootType == ShootType.Single)
        {
            this.isShooting = false;
        }

        if (this.currentWeapon.bulletsPerShot > 1)
        {
            StartCoroutine(this.FireBulletsPerShot());

            return;
        }

        this.FireSingleBullet();


    }

    private IEnumerator FireBulletsPerShot()
    {
        this.SetWeaponReady(false);

        for (int i = 1; i <= currentWeapon.bulletsPerShot; i++)
        {
            this.FireSingleBullet();

            yield return new WaitForSeconds(0.1f);

            if (i >= currentWeapon.bulletsPerShot)
                this.SetWeaponReady(true);
        }
    }

    private void FireSingleBullet()
    {
        this.currentWeapon.ammoesInMagazine--;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.BulletSingle, this.GunPoint().position, Quaternion.identity);
        newBullet.transform.rotation = Quaternion.LookRotation(this.GunPoint().forward);
        newBullet.gameObject.SetActive(true);

        BulletCtrl bulletCtrlScript = newBullet.GetComponent<BulletCtrl>();
    }

    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();

        this.controls.Character.Attack.performed += context => this.isShooting = true;
        this.controls.Character.Attack.canceled += context => this.isShooting = false;

        
    }
}
