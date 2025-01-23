using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : Spawner
{
    public static WeaponSpawner Instance => instance; 
    private static WeaponSpawner instance;

    public static string Axe_Weapon = "Axe";

    protected override void Awake()
    {
        base.Awake();
        if (WeaponSpawner.instance != null) Debug.LogError("Only 1 WeaponSpawner allow to exits!");
        WeaponSpawner.instance = this;
    }
}
