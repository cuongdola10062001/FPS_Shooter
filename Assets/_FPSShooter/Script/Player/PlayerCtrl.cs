using UnityEngine;

public class PlayerCtrl : ResetMonoBehaviour
{
    #region Variable Components
    public PlayerInputSystem_Actions controls { get; private set; }

    public CharacterController CharacterController => characterController;
    [SerializeField] protected CharacterController characterController;

    public Animator Anim => anim;
    [SerializeField] protected Animator anim;

    public PlayerAnimationEvents PlayerAnimationEvents => playerAnimationEvents;
    [SerializeField] protected PlayerAnimationEvents playerAnimationEvents;

    public PlayerAimController PlayerAimController => playerAimController;
    [SerializeField] protected PlayerAimController playerAimController;

    public PlayerMovement PlayerMovement => playerMovement;
    [SerializeField] protected PlayerMovement playerMovement;

    public PlayerAttack PlayerAttack => playerAttack;
    [SerializeField] protected PlayerAttack playerAttack;

    public PlayerWeaponController PlayerWeaponController => playerWeaponController;
    [SerializeField] protected PlayerWeaponController playerWeaponController;

    public PlayerWeaponVisuals PlayerWeaponVisuals => playerWeaponVisuals;
    [SerializeField] protected PlayerWeaponVisuals playerWeaponVisuals;

    public PlayerInteraction PlayerInteraction => playerInteraction;
    [SerializeField] protected PlayerInteraction playerInteraction;

    public PlayerSoundFX PlayerSoundFX => playerSoundFX;
    [SerializeField] protected PlayerSoundFX playerSoundFX;

    public WeaponHolder WeaponHolder => weaponHolder;
    [SerializeField] protected WeaponHolder weaponHolder;
    #endregion

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
        this.LoadPlayerAnimationEvents();
        this.LoadPlayerAimController();
        this.LoadPlayerMovement();
        this.LoadPlayerAttack();
        this.LoadPlayerWeaponController();
        this.LoadPlayerWeaponVisuals();
        this.LoadPlayerInteraction();
        this.LoadPlayerSoundFX();
        this.LoadWeaponHolder();
    }

    protected virtual void LoadCharacterController()
    {
        if (this.characterController != null) return;

        this.characterController = GetComponent<CharacterController>();
        Debug.LogWarning(transform.name + ": LoadCharacterController", gameObject);
    }
    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;

        this.anim = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadPlayerAnimationEvents()
    {
        if (this.playerAnimationEvents != null) return;

        this.playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();
        Debug.LogWarning(transform.name + ": LoadPlayerAnimationEvents", gameObject);
    }

    protected virtual void LoadPlayerAimController()
    {
        if (this.playerAimController != null) return;

        this.playerAimController = GetComponentInChildren<PlayerAimController>();
        Debug.LogWarning(transform.name + ": LoadPlayerAimController", gameObject);
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

    protected virtual void LoadPlayerWeaponController()
    {
        if (this.playerWeaponController != null) return;

        this.playerWeaponController = GetComponentInChildren<PlayerWeaponController>();
        Debug.LogWarning(transform.name + ": LoadPlayerWeaponController", gameObject);
    }

    protected virtual void LoadPlayerWeaponVisuals()
    {
        if (this.playerWeaponVisuals != null) return;

        this.playerWeaponVisuals = GetComponentInChildren<PlayerWeaponVisuals>();
        Debug.LogWarning(transform.name + ": LoadPlayerWeaponVisuals", gameObject);
    }

    protected virtual void LoadPlayerInteraction()
    {
        if (this.playerInteraction != null) return;

        this.playerInteraction = GetComponentInChildren<PlayerInteraction>();
        Debug.LogWarning(transform.name + ": LoadPlayerInteraction", gameObject);
    }

    protected virtual void LoadPlayerSoundFX()
    {
        if (this.playerSoundFX != null) return;

        this.playerSoundFX = GetComponentInChildren<PlayerSoundFX>();
        Debug.LogWarning(transform.name + ": LoadPlayerSoundFX", gameObject);
    }

    protected virtual void LoadWeaponHolder()
    {
        if (this.weaponHolder != null) return;

        this.weaponHolder = GetComponentInChildren<WeaponHolder>();
        Debug.LogWarning(transform.name + ": LoadWeaponHolder", gameObject);
    }
    #endregion
}
