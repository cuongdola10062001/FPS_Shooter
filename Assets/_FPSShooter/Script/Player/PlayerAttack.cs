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

        if (this.playerCtrl.PlayerWeaponController.CurrentWeapon.shootType == ShootType.Single)
        {
            this.isShooting = false;
        }

        if (this.playerCtrl.PlayerWeaponController.CurrentWeapon.bulletsPerShot > 1)
        {
            StartCoroutine(this.FireBulletsPerShot());

            return;
        }

        this.FireSingleBullet();


    }

    private IEnumerator FireBulletsPerShot()
    {
        this.playerCtrl.PlayerWeaponController.SetWeaponReady(false);

        for (int i = 1; i <= this.playerCtrl.PlayerWeaponController.CurrentWeapon.bulletsPerShot; i++)
        {
            this.FireSingleBullet();

            yield return new WaitForSeconds(0.1f);

            if (i >= this.playerCtrl.PlayerWeaponController.CurrentWeapon.bulletsPerShot)
                this.playerCtrl.PlayerWeaponController.SetWeaponReady(true);
        }
    }

    private void FireSingleBullet()
    {
        this.playerCtrl.PlayerWeaponController.CurrentWeapon.ammoesInMagazine--;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.BulletSingle, this.playerCtrl.PlayerWeaponController.GunPoint().position, Quaternion.identity);
        newBullet.transform.rotation = Quaternion.LookRotation(this.playerCtrl.PlayerWeaponController.GunPoint().forward);
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
