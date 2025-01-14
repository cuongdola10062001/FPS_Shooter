using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon System/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public WeaponType weaponType;
    public ShootType shootType;

    [Header("Bullet info")]
    public int bulletDamage;

    [Header("Regular shot")]
    public int bulletsPerShot = 1;
    public float fireRate;


    [Header("Magazine details")]
    public int ammoesInMagazine;
    public int capacityOfEachMagazine;
    public int totalReserveAmmo;

    [Header("Weapon generics")]
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
