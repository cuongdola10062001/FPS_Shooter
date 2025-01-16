using UnityEngine;

public enum EquipType
{
    SideEquipAnimation,
    BackEquipAnimation
};
public enum HoldType
{
    CommonHold = 1,
    LowHold = 2,
    HighHold = 3
};

public enum WeaponType
{
    Pistol = 1,
    Revolver = 2,
    AutoRifle = 3,
    Shotgun = 4,
    Rifle = 5
}

public enum ShootType
{
    Single = 1,
    Auto = 2
}

public class WeaponModel : ResetMonoBehaviour
{
    public WeaponData weaponData;

    public Transform gunPoint;
    public Transform pointStartGunBarrel;
    public Transform holdPoint;

    [Header("Audio")]
    public AudioSource fireSFX;
    public AudioSource realodSFX;

    [SerializeField] private int bulletDamage;
    [SerializeField] private int bulletsPerShot;
    public int BulletsPerShot=> bulletsPerShot;

    [SerializeField] private float fireRate;

    [SerializeField] private int ammoesInMagazine;
    [SerializeField] private int capacityOfEachMagazine;
    [SerializeField] private int totalReserveAmmo;

    [SerializeField] private float lastShootTime;

    public void ReduceAmmoesInMagazine() => this.ammoesInMagazine--;

    protected override void Start()
    {
        base.Start();

        this.bulletDamage = this.weaponData.bulletDamage;
        this.bulletsPerShot = this.weaponData.bulletsPerShot;
        this.fireRate = this.weaponData.fireRate;



        this.ammoesInMagazine = this.weaponData.ammoesInMagazine;
        this.capacityOfEachMagazine = this.weaponData.capacityOfEachMagazine;
        this.totalReserveAmmo = this.weaponData.totalReserveAmmo;
    }


    #region Weapon spread variables
    [Header("Spread ")]
    private float baseSpread = 1;
    private float maximumSpread = 5;
    private float currentSpread = 3;

    private float spreadIncreaseRate = .15f;

    private float lastSpreadUpdateTime;
    private float spreadCooldown = 1;

    public Vector3 ApplySpread(Vector3 originalDirection)
    {
        UpdateSpread();

        float randomizedValue = Random.Range(-currentSpread, currentSpread);

        Quaternion spreadRotation = Quaternion.Euler(randomizedValue, randomizedValue / 2, randomizedValue);

        return spreadRotation * originalDirection;
    }

    private void UpdateSpread()
    {
        if (Time.time > lastSpreadUpdateTime + spreadCooldown)
            currentSpread = baseSpread;
        else
            IncreaseSpread();

        lastSpreadUpdateTime = Time.time;
    }

    private void IncreaseSpread()
    {
        currentSpread = Mathf.Clamp(currentSpread + spreadIncreaseRate, baseSpread, maximumSpread);
    }
    #endregion



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
        this.LoadPointStartGunBarrel();
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

    protected virtual void LoadPointStartGunBarrel()
    {
        if (this.pointStartGunBarrel != null) return;

        this.pointStartGunBarrel = transform.Find("PointStartGunBarrel");
        Debug.LogWarning(transform.name + ": LoadPointStartGunBarrel", gameObject);
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
