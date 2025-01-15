using UnityEngine;

public class PlayerAimController : PlayerAbstract
{
    [SerializeField] protected LineRenderer aimLaser;

    /*[Header("Aim control")]
    public Transform Aim=> aim;
    [SerializeField] protected Transform aim;

    [Header("Camera control")]
    public Transform CameraTarget => cameraTarget;
    [SerializeField] protected Transform cameraTarget;*/
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

    protected virtual void Update()
    {

        /*this.UpdateAimPosition();
        this.UpdateCameraPosition();*/
    }

    protected virtual void UpdateAimVisuals()
    {
        this.aimLaser.enabled = this.playerCtrl.PlayerWeaponController.WeaponReady;

        if (this.aimLaser.enabled == false)
            return;

        //WeaponModel currentWeapon = this.playerCtrl.PlayerWeaponVisuals.cu
    }


    /*protected virtual void UpdateAimPosition()
    {
        this.aim.position = this.GetMouseHitInfo().point;

        this.aim.position = new Vector3(this.aim.position.x, transform.position.y + 1, this.aim.position.z);
    }

    protected virtual void UpdateCameraPosition()
    {
        this.cameraTarget.position = Vector3.Lerp(this.cameraTarget.position, this.DesieredCameraPosition(), this.cameraSensetivity * Time.deltaTime);
    }

    protected virtual Vector3 DesieredCameraPosition() // Vi tri camera mong muon
    {
        float actualMaxCameraDistance = this.playerCtrl.PlayerMovement.moveInput.y < -.5f ? this.minCameraDistance : this.maxCameraDistance;

        Vector3 desiredCameraPosition = this.GetMouseHitInfo().point;
        Vector3 aimDirection=(desiredCameraPosition-transform.position).normalized;

        float distanceToDesierdPosition = Vector3.Distance(transform.position, desiredCameraPosition);
        float clampedDistance = Mathf.Clamp(distanceToDesierdPosition, minCameraDistance, actualMaxCameraDistance);

        desiredCameraPosition = transform.position + aimDirection * clampedDistance;
        desiredCameraPosition.y = transform.position.y + 1;

        return desiredCameraPosition;
    }*/

    public virtual void EnableAimLaser(bool enable)=>this.aimLaser.enabled = enable;

    public virtual RaycastHit GetMouseHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(this.mouseInput);

        if(Physics.Raycast(ray,out var hitInfo, Mathf.Infinity, this.aimLayerMask))
        {
            this.lastKnownMouseHit = hitInfo;

            return hitInfo;
        }

        return lastKnownMouseHit;
    }

    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();

        this.controls.Character.Aim.performed += context => this.mouseInput = context.ReadValue<Vector2>();
        this.controls.Character.Aim.canceled += context => this.mouseInput = Vector2.zero;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAimLaser();
       /* this.LoadAim();
        this.LoadCameraTarget();*/
        this.LoadAimLayerMask();
    }

    protected virtual void LoadAimLaser()
    {
        if (this.aimLaser != null) return;

        this.aimLaser = GetComponentInChildren<LineRenderer>();
        Debug.LogWarning(transform.name + ": LoadAimLaser", gameObject);
    }

    /*protected virtual void LoadAim()
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
    }*/

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
