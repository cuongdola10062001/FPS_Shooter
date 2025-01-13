using UnityEngine;

public enum EquipType
{
    Null = 0,
    SideEquipAnimation = 1,
    BackEquipAnimation = 2
};
public enum HoldType
{
    Null = 0,
    CommonHold = 1,
    LowHold = 2,
    HighHold = 3
};

public class WeaponModel : ResetMonoBehaviour
{
    public WeaponType weaponType = WeaponType.Null;
    public EquipType equipType = EquipType.Null;
    public HoldType holdType = HoldType.Null;

    public Transform gunPoint;
    public Transform holdPoint;

    [Header("Audio")]
    public AudioSource fireSFX;
    public AudioSource realodSFX;


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWeaponType();
        this.LoadEquipType();
        this.LoadHoldType();
        this.LoadGunPoint();
        this.LoadHoldPoint();
        this.LoadFireSFX();
        this.LoadRealodSFX();
    }

    protected virtual void LoadWeaponType()
    {
        if (this.weaponType != WeaponType.Null) return;

        this.weaponType = System.Enum.TryParse(gameObject.name, out WeaponType parsedType) ? parsedType : WeaponType.Null;
        Debug.LogWarning(transform.name + ": LoadWeaponType", gameObject);
    }

    protected virtual void LoadEquipType()
    {
        if (this.equipType != EquipType.Null) return;

        if (this.weaponType == WeaponType.Pistol || this.weaponType == WeaponType.Revolver || this.weaponType == WeaponType.Shotgun)
        {
            this.equipType = EquipType.SideEquipAnimation;
        }
        else if (this.weaponType == WeaponType.AutoRifle || this.weaponType == WeaponType.Rifle)
        {
            this.equipType = EquipType.BackEquipAnimation;
        }
        else
        {
            this.equipType = EquipType.Null;
        }

        Debug.LogWarning(transform.name + ": LoadEquipType", gameObject);
    }

    protected virtual void LoadHoldType()
    {
        if (this.holdType != HoldType.Null) return;

        if (this.weaponType == WeaponType.Pistol || this.weaponType == WeaponType.Revolver || this.weaponType == WeaponType.AutoRifle)
        {
            this.holdType = HoldType.CommonHold;
        }
        else if (this.weaponType == WeaponType.Shotgun)
        {
            this.holdType = HoldType.LowHold;
        }
        else if (this.weaponType == WeaponType.Rifle)
        {
            this.holdType = HoldType.HighHold;
        }
        else
        {
            this.holdType = HoldType.Null;
        }

        Debug.LogWarning(transform.name + ": LoadHoldType", gameObject);
    }

    protected virtual void LoadGunPoint()
    {
        if (this.gunPoint != null) return;

        this.gunPoint = transform.Find("GunPoint");
        Debug.LogWarning(transform.name + ": LoadGunPoint", gameObject);
    }

    protected virtual void LoadHoldPoint()
    {
        if (this.holdPoint != null) return;

        this.holdPoint = transform.Find("HoldPoint");
        Debug.LogWarning(transform.name + ": LoadHoldPoint", gameObject);
    }

    protected virtual void LoadFireSFX()
    {
        if (this.fireSFX != null) return;

        this.fireSFX = transform.Find("FireSFX").GetComponent<AudioSource>();
        Debug.LogWarning(transform.name + ": LoadFireSFX", gameObject);
    }

    protected virtual void LoadRealodSFX()
    {
        if (this.realodSFX != null) return;

        this.realodSFX = transform.Find("RealodSFX").GetComponent<AudioSource>();

        Debug.LogWarning(transform.name + ": LoadRealodSFX", gameObject);
    }

    #endregion
}
