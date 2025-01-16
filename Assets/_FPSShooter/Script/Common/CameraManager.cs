using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : ResetMonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance => instance;



    [SerializeField] protected CinemachineCamera virtualCamera;
    [SerializeField] protected CinemachinePositionComposer transposer;


    [Header("Camera distance")]
    [SerializeField] protected float distanceChangeRate = .75f;
    [SerializeField] protected float targetCameraDistance = 7f;

    protected override void Awake()
    {
        base.Awake();
        if (CameraManager.instance != null) Debug.LogError("Only 1 CameraManager allow to exits!");
        CameraManager.instance = this;
    }

    protected virtual void Update()
    {
        this.UpdateCameraDistance();
    }

    private void UpdateCameraDistance()
    {
        float currentDistnace = transposer.CameraDistance;

        if (Mathf.Abs(targetCameraDistance - currentDistnace) < .01f)
            return;

        transposer.CameraDistance =
            Mathf.Lerp(currentDistnace, targetCameraDistance, distanceChangeRate * Time.deltaTime);
    }

    public virtual void ChangeCameraDistance(float distance) => this.targetCameraDistance = distance;

    public void ChangeCameraTarget(Transform target, float cameraDistance = 10, float newLookAheadTime = 0)
    {
        virtualCamera.Follow = target;
        transposer.Lookahead.Time = newLookAheadTime;
        ChangeCameraDistance(cameraDistance);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadVirtualCamera();
        this.LoadTransposer();
    }

    protected virtual void LoadVirtualCamera()
    {
        if (this.virtualCamera != null) return;

        this.virtualCamera = GetComponentInChildren<CinemachineCamera>();
        Debug.LogWarning(transform.name + ": LoadVirtualCamera", gameObject);
    }

    protected virtual void LoadTransposer()
    {
        if (this.transposer != null) return;

        this.transposer = GetComponentInChildren<CinemachinePositionComposer>();
        Debug.LogWarning(transform.name + ": LoadTransposer", gameObject);
    }
    #endregion

}
