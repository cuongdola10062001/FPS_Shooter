using System;
using UnityEngine;

public enum WeaponType
{
    Null = 0,
    Pistol = 1,
    Revolver = 2,
    AutoRifle = 3,
    Shotgun = 4,
    Rifle = 5
}

public enum ShootType
{
    Single,
    Auto
}

[Serializable]
public class Weapon
{
    public WeaponType weaponType;
    public ShootType shootType;

    [Header("Bullet info")]
    public int bulletDamage;

    public int bulletsPerShot;

    public float fireRate = 1;
    private float defaultFireRate;
    private float lastShootTime;


    [Header("Magazine details")]
    public int ammoesInMagazine;
    public int capacityOfEachMagazine;
    public int totalReserveAmmo;

    #region Weapon generic info variables
    public float reloadSpeed; // how fast charcater reloads weapon    
    public float equipmentSpeed; // how fast character equips weapon
    public float gunDistance;
    public float cameraDistance;
    #endregion

    public WeaponData weaponData; // serves as default weapon data

    public Weapon(WeaponData weaponData)
    {
        this.weaponData = weaponData;

        this.weaponType = weaponData.weaponType;
        this.shootType = weaponData.shootType;

        this.bulletDamage = weaponData.bulletDamage;

        this.bulletsPerShot = weaponData.bulletsPerShot;

        this.ammoesInMagazine = weaponData.ammoesInMagazine;
        this.capacityOfEachMagazine = weaponData.capacityOfEachMagazine;
        this.totalReserveAmmo = weaponData.totalReserveAmmo;

        this.reloadSpeed = weaponData.reloadSpeed;
        this.equipmentSpeed = weaponData.equipmentSpeed;
        this.gunDistance = weaponData.gunDistance;
        this.cameraDistance = weaponData.cameraDistance;
    }

    public bool CanShoot() => this.HaveEnoughBullets() && this.ReadyToFire();
    private bool ReadyToFire()
    {
        if (Time.time > this.lastShootTime + 1 / this.fireRate)
        {
            this.lastShootTime = Time.time;
            return true;
        }
        return false;
    }
    private bool HaveEnoughBullets() => this.ammoesInMagazine > 0;

    

    public bool CanReload()
    {
        if (this.ammoesInMagazine == this.capacityOfEachMagazine)
            return false;

        if(this.totalReserveAmmo>0)
            return true;

        return false;
    }
    public void RefillBullets()
    {
        int bulletsToReload = this.capacityOfEachMagazine;

        if (bulletsToReload > totalReserveAmmo)
        {
            bulletsToReload = totalReserveAmmo;
        }

        totalReserveAmmo -= bulletsToReload;
        this.ammoesInMagazine = bulletsToReload;

        if (this.totalReserveAmmo < 0)
            this.totalReserveAmmo = 0;
    }

}
