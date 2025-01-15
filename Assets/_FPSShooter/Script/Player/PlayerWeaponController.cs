using UnityEngine;

public class PlayerWeaponController : PlayerAbstract
{
    public WeaponModel CurrentWeapon => currentWeapon;
    [SerializeField] protected WeaponModel currentWeapon;

    public bool WeaponReady => weaponReady;
    [SerializeField] protected bool weaponReady;

    [Header("Bullet details")]
    [SerializeField] protected float bulletImpactForce = 100;
    [SerializeField] protected float bulletSpeed;

    public void SetWeaponReady(bool ready)
    {
        this.weaponReady = ready;
    }
    public Transform GunPoint() => this.playerCtrl.PlayerWeaponVisuals.CurrentWeaponModel().gunPoint;

    protected override void Start()
    {
        base.Start();

        this.weaponReady = true;//setting cho lan Equip dau tien
        this.EquipWeapon(0);
    }


    protected virtual void EquipWeapon(int i)
    {
        if (this.weaponReady == false) return;
        this.weaponReady = false;

        this.currentWeapon = this.playerCtrl.WeaponHolder.WeaponModels[i];
        this.playerCtrl.PlayerWeaponVisuals.PlayWeaponEquipAnimation();

    }

    private void Reload()
    {
        if (this.weaponReady == false) return;
        this.weaponReady = false;

        this.playerCtrl.PlayerWeaponVisuals.PlayReloadAnimation();
    }


    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();


        this.controls.Character.EquipSlot1.performed += context => this.EquipWeapon(0);
        this.controls.Character.EquipSlot2.performed += context => this.EquipWeapon(1);
        this.controls.Character.EquipSlot3.performed += context => this.EquipWeapon(2);
        this.controls.Character.EquipSlot4.performed += context => this.EquipWeapon(3);
        this.controls.Character.EquipSlot5.performed += context => this.EquipWeapon(4);


        this.controls.Character.Reload.performed += context =>
        {
            if (this.currentWeapon.CanReload() && this.weaponReady)
            {
                this.Reload();
            }
        };
    }
}
