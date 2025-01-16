using UnityEngine;

public class FXDespawnByTime : DespawnByTime
{
    protected override void ResetValue()
    {
        base.ResetValue();

        this.delay = 1f;
    }

    public override void DespawnObject()
    {
        FXSpawner.Instance.Despawn(transform.parent);
    }
}
