using UnityEngine;

public class PlayerAbstract : ResetMonoBehaviour
{
    protected PlayerInputSystem_Actions controls;

    [SerializeField] protected PlayerCtrl playerCtrl;


    protected override void Start()
    {
        base.Start();
        

        this.AssignInputEvents();
    }

    protected virtual void AssignInputEvents()
    {
        this.controls = this.playerCtrl.controls;

    }


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;

        this.playerCtrl=GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }
    #endregion
}
