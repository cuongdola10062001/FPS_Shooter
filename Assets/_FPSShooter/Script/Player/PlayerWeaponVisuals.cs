using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponVisuals : PlayerAbstract
{

    //[SerializeField] protected WeaponModel[] weaponModels;
    //[SerializeField] protected BackupWeaponModel[] backupWeaponModels;


    [Header("Rig ")]
    [SerializeField] protected float rigWeightIncreaseRate = 2.75f;
    protected bool shouldIncrease_RigWeight;
    //protected Rig rig;

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

    public void PlayWeaponEquipAnimation()
    {
        EquipType equipType = CurrentWeaponModel().weaponData.equipType;

        float equipmentSpeed = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.equipmentSpeed;

        this.playerCtrl.LeftHandIK.weight = 0;
        this.ReduceRigWeight();
        this.playerCtrl.Anim.SetTrigger("EquipWeapon");
        this.playerCtrl.Anim.SetFloat("EquipType", ((float)equipType));
        this.playerCtrl.Anim.SetFloat("EquipSpeed", equipmentSpeed);
    }

    public void SwitchOnCurrentWeaponModel()
    {
        int animationIndex = ((int)CurrentWeaponModel().weaponData.holdType);

        this.SwitchOffWeaponModels();
        this.SwitchAnimationLayer(animationIndex);
        this.CurrentWeaponModel().gameObject.SetActive(true);
        this.AttachLeftHand();
    }
    public void SwitchOffWeaponModels()
    {
        for (int i = 0; i <this.playerCtrl.WeaponHolder.WeaponModels.Length; i++)
        {
            this.playerCtrl.WeaponHolder.WeaponModels[i].gameObject.SetActive(false);
        }
    }
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

            if (backupModel.weaponType == player.weapon.CurrentWeapon().weaponType)
                continue;


            if (player.weapon.WeaponInSlots(backupModel.weaponType) != null)
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

    public virtual WeaponModel CurrentWeaponModel()
    {
        WeaponModel weaponModel = null;

        WeaponType weaponType = this.playerCtrl.PlayerWeaponController.CurrentWeapon.weaponData.weaponType;

        for (int i = 0; i <this.playerCtrl.WeaponHolder.WeaponModels.Length; i++)
        {
            if (this.playerCtrl.WeaponHolder.WeaponModels[i].weaponData.weaponType == weaponType)
                weaponModel = this.playerCtrl.WeaponHolder.WeaponModels[i];
        }

        return weaponModel;
    }


    #region Animation Rigging Methods

    protected virtual void AttachLeftHand()
    {
        Transform targetTransform = CurrentWeaponModel().holdPoint;

        this.playerCtrl.LeftHandIKTarget.position = CurrentWeaponModel().holdPoint.position;
        this.playerCtrl.LeftHandIKTarget.rotation = CurrentWeaponModel().holdPoint.rotation;
    }

    protected virtual void UpdateLeftHandIKWeight()
    {
        if (shouldIncrease_LeftHandIKWieght)
        {
            this.playerCtrl.LeftHandIK.weight += leftHandIkWeightIncreaseRate * Time.deltaTime;

            if (this.playerCtrl.LeftHandIK.weight >= 1)
                shouldIncrease_LeftHandIKWieght = false;
        }
    }
    protected virtual void UpdateRigWigth()
    {
        if (shouldIncrease_RigWeight)
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

    public virtual void MaximizeRigWeight() => shouldIncrease_RigWeight = true;
    public virtual void MaximizeLeftHandWeight() => shouldIncrease_LeftHandIKWieght = true;

    #endregion


}
