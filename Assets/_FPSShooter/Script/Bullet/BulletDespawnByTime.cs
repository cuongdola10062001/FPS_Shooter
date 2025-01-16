using UnityEngine;

public class BulletDespawnByTime : DespawnByTime
{


    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
