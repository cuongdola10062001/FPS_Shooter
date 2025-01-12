using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : ResetMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }
    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }
 
    public virtual void DespawnObject()
    {
        Debug.Log(transform.name + ": DespawnObject", gameObject);
    }

    protected abstract bool CanDespawn();
}
