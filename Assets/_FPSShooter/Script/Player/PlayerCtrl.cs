using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerCtrl : ResetMonoBehaviour
{
    public PlayerInputSystem_Actions controls { get; private set; }

    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected PlayerAttack playerAttack;

    protected override void Awake()
    {
        base.Awake();

        controls = new PlayerInputSystem_Actions();
    }


   
    protected override void OnEnable()
    {
        base.OnEnable();

        controls.Enable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        controls.Disable();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadPlayerMovement();
        this.LoadPlayerAttack();
    }

    protected virtual void LoadCharacterController()
    {
        if (this.characterController != null) return;

        this.characterController = GetComponent<CharacterController>();
        Debug.LogWarning(transform.name + ": LoadCharacterController", gameObject);
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;

        this.animator = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;

        this.playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }
    protected virtual void LoadPlayerAttack()
    {
        if (this.playerAttack != null) return;

        this.playerAttack = GetComponentInChildren<PlayerAttack>();
        Debug.LogWarning(transform.name + ": LoadPlayerAttack", gameObject);
    }
    #endregion
}
