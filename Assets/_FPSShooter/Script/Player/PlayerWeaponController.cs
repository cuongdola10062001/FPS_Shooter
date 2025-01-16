using UnityEngine;

public class PlayerWeaponController : PlayerAbstract
{
    public WeaponModel CurrentWeapon => currentWeapon;
    [SerializeField] protected WeaponModel currentWeapon;

    private bool weaponReady;
    public bool WeaponReady => weaponReady;
    public void SetWeaponReady(bool ready)=> this.weaponReady = ready;

    protected bool canEquip = false;
    public bool CanEquip=>canEquip;
    public void SetCanEquip(bool canEquip)=>this.canEquip = canEquip;



    protected override void Start()
    {
        base.Start();

        this.canEquip = true;
        this.EquipWeapon(0);
    }


    protected virtual void EquipWeapon(int i)
    {
        if (!this.canEquip) return;
        this.weaponReady = false;
        this.canEquip = false;

        this.currentWeapon = this.playerCtrl.WeaponHolder.WeaponModels[i];
        this.playerCtrl.PlayerWeaponVisuals.PlayWeaponEquipAnimation();

        CameraManager.Instance.ChangeCameraDistance(this.currentWeapon.weaponData.cameraDistance);
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
