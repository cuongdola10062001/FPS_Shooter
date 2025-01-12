using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : ResetMonoBehaviour
{
    [SerializeField] protected Transform holder;

    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount { get => spawnedCount;}

    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;

    #region Spawn Obj
    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
       Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        return this.Spawn(prefab, spawnPos, rotation);
    }

    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        newPrefab.SetParent(this.holder);
        this.spawnedCount++;

        return newPrefab;
    }

    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }

        return null;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if(poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    #endregion


    public virtual Transform RandomPrefab()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }


    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;

        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnedCount--;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        this.LoadPrefabs();
        this.LoadHodler();
    }

    protected virtual void LoadHodler()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.LogWarning(transform.name + ": LoadHodler", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }

        this.HidePrefabs();

        Debug.LogWarning(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    #endregion

}
