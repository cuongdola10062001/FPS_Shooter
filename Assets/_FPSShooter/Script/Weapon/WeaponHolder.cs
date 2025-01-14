using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : ResetMonoBehaviour
{
    [SerializeField] protected WeaponModel[] weaponModels;
    public WeaponModel[] WeaponModels => weaponModels;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWeaponModels();
    }

    protected virtual void LoadWeaponModels()
    {
        if (this.weaponModels.Length > 0) return;

        this.weaponModels = GetComponentsInChildren<WeaponModel>(true);
        Debug.LogWarning(transform.name + ": LoadWeaponModels", gameObject);
    }
    #endregion
}
