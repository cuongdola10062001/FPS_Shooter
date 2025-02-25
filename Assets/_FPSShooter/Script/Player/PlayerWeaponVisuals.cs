using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponVisuals : PlayerAbstract
{
    [Header("Rig ")]
    [SerializeField] protected float rigWeightIncreaseRate = 2.75f;
    protected bool shouldIncrease_RigWeight;

    [Header("Left hand IK")]
    [SerializeField] protected float leftHandIkWeightIncreaseRate = 2.5f;
    protected bool shouldIncrease_LeftHandIKWieght;

    protected virtual void Update()
    {
        this.UpdateRigWigth();
        this.UpdateLeftHandIKWeight();
    }


    public void PlayFireAnimation() =>this.playerCtrl.Anim.SetTrigger("Fire");
    public void PlayReloadAnimation()
    {
        float reloadSpeed = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.reloadSpeed;

        this.playerCtrl.Anim.SetFloat("ReloadSpeed", reloadSpeed);
        this.playerCtrl.Anim.SetTrigger("Reload");
        this.ReduceRigWeight();
    }

    #region Weapon Equip Animation
    public void PlayWeaponEquipAnimation()
    {
        EquipType equipType = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.equipType;

        float equipmentSpeed = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.equipmentSpeed;

        this.playerCtrl.LeftHandIK.weight = 0;
        this.ReduceRigWeight();

        this.playerCtrl.Anim.SetTrigger("EquipWeapon");
        this.playerCtrl.Anim.SetFloat("EquipType", ((float)equipType));
        this.playerCtrl.Anim.SetFloat("EquipSpeed", equipmentSpeed);
    }

    public void SwitchOnCurrentWeaponModel()
    {
        int animationIndex = ((int)this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.holdType);

        this.SwitchOffWeaponModels();
        this.SwitchAnimationLayer(animationIndex);
        this.playerCtrl.PlayerWeaponController.CurrentWeapon.gameObject.SetActive(true);
        this.AttachLeftHand();
    }
    public void SwitchOffWeaponModels()
    {
        for (int i = 0; i <this.playerCtrl.WeaponHolder.WeaponModels.Length; i++)
        {
            this.playerCtrl.WeaponHolder.WeaponModels[i].gameObject.SetActive(false);
        }
    }
    #endregion


    /*private void SwitchOffBackupWeaponModels()
    {
        foreach (BackupWeaponModel backupModel in backupWeaponModels)
        {
            backupModel.Activate(false);
        }
    }*/

    /*public void SwitchOnBackupWeaponModel()
    {
        SwitchOffBackupWeaponModels();

        BackupWeaponModel lowHangWeapon = null;
        BackupWeaponModel backHangWeapon = null;
        BackupWeaponModel sideHangWeapon = null;

        foreach (BackupWeaponModel backupModel in backupWeaponModels)
        {

            if (backupModel.weaponGunType == player.weapon.CurrentWeapon().weaponGunType)
                continue;


            if (player.weapon.WeaponInSlots(backupModel.weaponGunType) != null)
            {
                if (backupModel.HangTypeIs(HangType.LowBackHang))
                    lowHangWeapon = backupModel;

                if (backupModel.HangTypeIs(HangType.BackHang))
                    backHangWeapon = backupModel;

                if (backupModel.HangTypeIs(HangType.SideHang))
                    sideHangWeapon = backupModel;
            }
        }

        lowHangWeapon?.Activate(true);
        backHangWeapon?.Activate(true);
        sideHangWeapon?.Activate(true);
    }*/


    protected virtual void SwitchAnimationLayer(int layerIndex)
    {
        for (int i = 1; i < this.playerCtrl.Anim.layerCount; i++)
        {
            this.playerCtrl.Anim.SetLayerWeight(i, 0);
        }

        this.playerCtrl.Anim.SetLayerWeight(layerIndex, 1);
    }

    #region Animation Rigging Methods

    protected virtual void AttachLeftHand()
    {
        Transform targetTransform = this.playerCtrl.PlayerWeaponController.CurrentWeapon.holdPoint;

        this.playerCtrl.LeftHandIKTarget.position = this.playerCtrl.PlayerWeaponController.CurrentWeapon.holdPoint.position;
        this.playerCtrl.LeftHandIKTarget.rotation = this.playerCtrl.PlayerWeaponController.CurrentWeapon.holdPoint.rotation;
    }

    protected virtual void UpdateLeftHandIKWeight()
    {
        if (this.shouldIncrease_LeftHandIKWieght)
        {
            this.playerCtrl.LeftHandIK.weight += leftHandIkWeightIncreaseRate * Time.deltaTime;

            if (this.playerCtrl.LeftHandIK.weight >= 1)
                shouldIncrease_LeftHandIKWieght = false;
        }
    }
    protected virtual void UpdateRigWigth()
    {
        if (this.shouldIncrease_RigWeight)
        {
            this.playerCtrl.Rig.weight += rigWeightIncreaseRate * Time.deltaTime;

            if (this.playerCtrl.Rig.weight >= 1)
                shouldIncrease_RigWeight = false;
        }
    }
    protected virtual void ReduceRigWeight()
    {
        this.playerCtrl.Rig.weight = .15f;
    }

    public virtual void MaximizeRigWeight() => this.shouldIncrease_RigWeight = true;
    public virtual void MaximizeLeftHandWeight() => this.shouldIncrease_LeftHandIKWieght = true;

    #endregion


}
