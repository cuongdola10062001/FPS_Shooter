using UnityEngine;

public class BulletDamageSender : DamageSender
{
    protected override string GetImpactFX()
    {
        return FXSpawner.BulletVFX;
    }
}
