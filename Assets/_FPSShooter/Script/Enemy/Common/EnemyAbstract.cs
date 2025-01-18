using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyAbstract : ResetMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;

        this.enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.LogWarning(transform.name + ": LoadEnemyCtrl", gameObject);
    }
    #endregion

}
