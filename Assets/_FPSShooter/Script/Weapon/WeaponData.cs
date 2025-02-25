using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon System/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    public EquipType equipType ;
    public HoldType holdType;

    [Header("Melee Attributes")]
    public WeaponMeleeType meleeType;
    public int meleeDamage;
    public float meleeRange;
    public float attackSpeed;

    [Header("Gun Attributes")]
    public WeaponGunType weaponGunType;
    public ShootType shootType;
    public int bulletDamage;
    public float shootingDistanceLimit;
    public int bulletsPerShot = 1;
    public float fireRate;
    public int ammoesInMagazine;
    public int capacityOfEachMagazine;
    public int totalReserveAmmo;

    [Header("Weapon Speed & Distance")]
    [Range(1, 3)]
    public float reloadSpeed = 1;
    [Range(1, 3)]
    public float equipmentSpeed = 1;
    [Range(4, 25)]
    public float gunDistance = 4;
    [Range(4, 10)]
    public float cameraDistance = 6;

    [Header("UI elements")]
    public Sprite weaponIcon;
    public string weaponInfo;
}


