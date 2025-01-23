using UnityEngine;

public class AxeWeapon : ResetMonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform axeVisual;

    private Vector3 direction;
    private Transform target;
    private float flySpeed;
    private float rotationSpeed;
    private float timer = 1;

    private int damage;

    public void AxeSetup(float flySpeed, Transform target, float timer, int damage)
    {
        this.rotationSpeed = 1600;

        this.damage = damage;
        this.target = target;
        this.flySpeed = flySpeed;
        this.timer = timer;
    }

    private void Update()
    {
        this.axeVisual.Rotate(Vector3.right*this.rotationSpeed*Time.deltaTime);
        this.timer-= Time.deltaTime;

        if (this.timer > 0)
            this.direction = this.target.position + Vector3.up - transform.position;

        transform.forward = this.rb.linearVelocity;
    }

    private void FixedUpdate()
    {
        this.rb.linearVelocity = this.direction.normalized * this.flySpeed;
    }



    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadRigidbody();
        this.LoadAxeVisual();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody>();
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadAxeVisual()
    {
        if (this.axeVisual != null) return;
        this.axeVisual = transform.Find("Visuals");
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }
    #endregion
}

