using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 70f;
    [SerializeField] protected float distance = 0f;

    [SerializeField] private Vector3 startPosition;

    protected override void OnEnable()
    {
        base.OnEnable();

        this.ResetStartPosition();
    }

    protected virtual void ResetStartPosition()
    {
        this.startPosition = transform.position;
    }


    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.position, this.startPosition);
        if(this.distance > disLimit) return true;
        return false;
    }
}
