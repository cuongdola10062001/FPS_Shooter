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

    [Header("Bullet details")]
    [SerializeField] protected float bulletImpactForce = 100;
    [SerializeField] protected float bulletSpeed;

    public void SetWeaponReady(bool ready)
    {
        this.weaponReady = ready;
    }
    public Transform GunPoint() => this.playerCtrl.PlayerWeaponVisuals.CurrentWeaponModel().gunPoint;


    protected virtual void EquipWeapon(int i)
    {
        this.SetWeaponReady(false);

        //this.currentWeapon = this.playerCtrl.WeaponHolder.WeaponModels[i];
    }

    
    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();


        
    }
}
