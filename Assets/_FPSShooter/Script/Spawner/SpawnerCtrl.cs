using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : ResetMonoBehaviour
{
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner  => spawner; 


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    
}
