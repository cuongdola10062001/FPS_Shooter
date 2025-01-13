using UnityEngine;

public class PlayerAimController : PlayerAbstract
{
    [SerializeField] protected LineRenderer aimLaser;

    [Header("Aim control")]
    [SerializeField] protected Transform aim;

    [SerializeField] protected bool isAimingPrecisly;
    [SerializeField] protected bool isLockingToTarget;

    [Header("Camera control")]
    [SerializeField] protected Transform cameraTarget;
    [Range(.5f, 1)]
    [SerializeField] protected float minCameraDistance = 1.5f;
    [Range(1, 3f)]
    [SerializeField] protected float maxCameraDistance = 4;
    [Range(3f, 5f)]
    [SerializeField] protected float cameraSensetivity = 5f;

    [Space]

    [SerializeField] protected LayerMask aimLayerMask;

    protected Vector2 mouseInput;
    protected RaycastHit lastKnownMouseHit;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAimLaser();
        this.LoadAim();
        this.LoadCameraTarget();
        this.LoadAimLayerMask();
    }

    protected virtual void LoadAimLaser()
    {
        if (this.aimLaser != null) return;

        this.aimLaser = GetComponentInChildren<LineRenderer>();
        Debug.LogWarning(transform.name + ": LoadAimLaser", gameObject);
    }

    protected virtual void LoadAim()
    {
        if (this.aim != null) return;

        this.aim = GameObject.Find("AimTarget").transform;
        Debug.LogWarning(transform.name + ": LoadAim", gameObject);
    }

    protected virtual void LoadCameraTarget()
    {
        if (this.cameraTarget != null) return;

        this.cameraTarget = GameObject.Find("CameraFollowTarget").transform;
        Debug.LogWarning(transform.name + ": LoadCameraTarget", gameObject);
    }

    protected virtual void LoadAimLayerMask()
    {
        if (this.aimLayerMask.value != 0) return;

        int playerLayer = LayerMask.NameToLayer("Player");
        /*int enemyLayer = LayerMask.NameToLayer("Enemy");
        int waterLayer = LayerMask.NameToLayer("Water");*/

        //this.aimLayerMask = ~((1 << playerLayer) | (1 << enemyLayer) | (1 << waterLayer));
        this.aimLayerMask = ~(1 << playerLayer);
        Debug.LogWarning(transform.name + ": LoadAimLayerMask", gameObject);
    }
    #endregion
}
