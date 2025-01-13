using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : ResetMonoBehaviour
{
    [SerializeField] protected List<WeaponModel> weaponModels = new List<WeaponModel>();
    public List<WeaponModel> WeaponModels => weaponModels;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWeaponModels();
    }

    protected virtual void LoadWeaponModels()
    {
        if (this.weaponModels.Count > 0) return;

        this.weaponModels = GetComponentsInChildren<WeaponModel>().ToList();
        Debug.LogWarning(transform.name + ": LoadWeaponModels", gameObject);
    }
    #endregion
}
