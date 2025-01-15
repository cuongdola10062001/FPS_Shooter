using UnityEngine;

public enum EquipType
{
    SideEquipAnimation = 1,
    BackEquipAnimation = 2
};
public enum HoldType
{
    CommonHold = 1,
    LowHold = 2,
    HighHold = 3
};

public class WeaponModel : ResetMonoBehaviour
{
    public WeaponData weaponData;

    public Transform gunPoint;
    public Transform holdPoint;

    [Header("Audio")]
    public AudioSource fireSFX;
    public AudioSource realodSFX;


    private float fireRate;
    private float lastShootTime;

    private int ammoesInMagazine;
    private int capacityOfEachMagazine;
    private int totalReserveAmmo;

    protected override void Start()
    {
        base.Start();

        this.fireRate = this.weaponData.fireRate;
        this.ammoesInMagazine = this.weaponData.ammoesInMagazine;
        this.capacityOfEachMagazine = this.weaponData.capacityOfEachMagazine;
        this.totalReserveAmmo = this.weaponData.totalReserveAmmo;
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

        if (this.totalReserveAmmo > 0)
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

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadWeaponData();
        this.LoadGunPoint();
        this.LoadHoldPoint();
        this.LoadFireSFX();
        this.LoadRealodSFX();
    }

    protected virtual void LoadWeaponData()
    {
        if (this.weaponData != null) return;

        string resPath = "Weapon/" + "Weapon_" + transform.name;
        this.weaponData = Resources.Load<WeaponData>(resPath);

        Debug.LogWarning(transform.name + ": LoadWeaponData", gameObject);
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
