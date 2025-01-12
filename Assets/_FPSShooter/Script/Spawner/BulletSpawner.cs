using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    public static BulletSpawner Instance => instance; 
    private static BulletSpawner instance;

    public static string bulletOne = "Bullet_1";

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogError("Only 1 BulletSpawner allow to exits!");
        BulletSpawner.instance = this;
    }
}
