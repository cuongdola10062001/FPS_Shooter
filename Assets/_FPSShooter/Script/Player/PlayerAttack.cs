using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerAbstract
{
    public bool IsShooting => isShooting;
    protected bool isShooting;

    protected virtual void Update()
    {
        if (this.isShooting)
        {
            this.Shoot();
        }
    }

    protected virtual void Shoot()
    {
        if (!this.playerCtrl.PlayerWeaponController.WeaponReady) return;

        if (!this.playerCtrl.PlayerWeaponController.CurrentWeapon.CanShoot()) return;

        this.playerCtrl.PlayerWeaponVisuals.PlayFireAnimation();

        if (this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.shootType == ShootType.Single)
        {
            this.isShooting = false;
        }

        if (this.playerCtrl.PlayerWeaponController.CurrentWeapon.BulletsPerShot > 1)
        {
            StartCoroutine(this.FireBulletsPerShot());

            return;
        }

        this.FireSingleBullet();


    }

    private IEnumerator FireBulletsPerShot()
    {
        this.playerCtrl.PlayerWeaponController.SetWeaponReady(false);

        for (int i = 1; i <= this.playerCtrl.PlayerWeaponController.CurrentWeapon.BulletsPerShot; i++)
        {
            this.FireSingleBullet();

            yield return new WaitForSeconds(0.1f);

            if (i >= this.playerCtrl.PlayerWeaponController.CurrentWeapon.BulletsPerShot)
                this.playerCtrl.PlayerWeaponController.SetWeaponReady(true);
        }
    }


    private void FireSingleBullet()
    {
        this.playerCtrl.PlayerWeaponController.CurrentWeapon.ReduceAmmoesInMagazine();

        Transform gunPoint = this.playerCtrl.PlayerWeaponController.CurrentWeapon.gunPoint;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.BulletSingle, gunPoint.position, Quaternion.identity);
        newBullet.transform.rotation = Quaternion.LookRotation(gunPoint.forward);

        Vector3 bulletDirection = this.playerCtrl.PlayerWeaponController.CurrentWeapon.ApplySpread(this.BulletDirection());
        BulletCtrl bulletCtrlScript = newBullet.GetComponent<BulletCtrl>();

        WeaponData weaponData = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData;
        bulletCtrlScript.Bullet.SetupBullet
            (weaponData.bulletDamage, bulletDirection, weaponData.shootingDistanceLimit);

        newBullet.gameObject.SetActive(true); // khi enable bullet se tu dong bay
    }

    public virtual Vector3 BulletDirection()
    {
        Vector3 pointStart = this.playerCtrl.PlayerWeaponController.CurrentWeapon.pointStartGunBarrel.position;
        Vector3 pointEnd = this.playerCtrl.PlayerWeaponController.CurrentWeapon.gunPoint.position;

        Vector3 direction = (pointEnd - pointStart).normalized;


        return direction;
    }

    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();

        this.controls.Character.Attack.performed += context => this.isShooting = true;
        this.controls.Character.Attack.canceled += context => this.isShooting = false;


    }
}
