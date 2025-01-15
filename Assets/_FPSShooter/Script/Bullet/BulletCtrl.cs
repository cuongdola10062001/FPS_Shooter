using UnityEngine;

public class BulletCtrl : ResetMonoBehaviour
{
    [SerializeField] protected Collider cd;
    public Collider Cd=> cd;

    [SerializeField] protected Rigidbody rb;
    public Rigidbody Rb => rb;


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadCollider()
    {
        if (this.cd != null) return;

        this.cd = GetComponent<Collider>();
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;

        this.rb = GetComponent<Rigidbody>();
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }
    #endregion


}
