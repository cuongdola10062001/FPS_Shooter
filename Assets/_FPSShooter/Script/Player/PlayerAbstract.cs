using UnityEngine;

public class PlayerAbstract : ResetMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;


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
}
