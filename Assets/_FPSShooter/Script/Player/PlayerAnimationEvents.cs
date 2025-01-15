using UnityEngine;

public class PlayerAnimationEvents : PlayerAbstract
{
    public void ReloadIsOver()
    {
        this.playerCtrl.PlayerWeaponVisuals.MaximizeRigWeight();

        this.playerCtrl.PlayerWeaponController.CurrentWeapon.RefillBullets();


        this.playerCtrl.PlayerWeaponController.SetWeaponReady(true);
    }


    public void ReturnRig()
    {
        this.playerCtrl.PlayerWeaponVisuals.MaximizeRigWeight();
        this.playerCtrl.PlayerWeaponVisuals.MaximizeLeftHandWeight();
    }

    public void WeaponEquipingIsOver()
    {
        this.playerCtrl.PlayerWeaponController.SetWeaponReady(true);
    }

    public void SwitchOnWeaponModel() => this.playerCtrl.PlayerWeaponVisuals.SwitchOnCurrentWeaponModel();
}
