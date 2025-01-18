using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : EnemyAbstract
{
    [Header("Move data")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3;
    public float turnSpeed;
    private bool manualMovement;
    private bool manualRotation;

    public virtual void FaceTarget(Vector3 target,float turnSpeed = 0)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target-transform.parent.position);

        Vector3 currentEulerAngels = transform.parent.rotation.eulerAngles;

        if (turnSpeed == 0)
            turnSpeed = this.turnSpeed;

        float yRotation = Mathf.LerpAngle(currentEulerAngels.y,targetRotation.eulerAngles.y,turnSpeed*Time.deltaTime);

        transform.parent.rotation = Quaternion.Euler(currentEulerAngels.x, yRotation, currentEulerAngels.z);
    }

    public virtual void ActivateManualMovement(bool manualMovement) => this.manualMovement = manualMovement;
    public virtual bool ManualMovementActive() => manualMovement;

    public virtual void ActivateManualRotation(bool manualRotation) => this.manualRotation = manualRotation;
    public virtual bool ManualRotationActive() => manualRotation;
}
