using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [Header("Movement info")]
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float turnSpeed;
    protected float speed;
    protected float verticalVelocity;


    public Vector2 moveInput { get; protected set; }
    protected Vector3 movementDirection;

    protected bool isRunning;

    protected override void Start()
    {
        base.Start();

        this.speed = this.walkSpeed;
    }

    protected virtual void Update()
    {
        this.ApplyMovement();
        this.ApplyRotation();
        this.AnimationControllers();
    }

    protected virtual void ApplyMovement()
    {
        this.movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        this.ApplyGravity();

        if (this.movementDirection.magnitude > 0)
        {

            this.playerCtrl.CharacterController.Move(this.movementDirection * Time.deltaTime * speed);
        }
    }
    protected virtual void ApplyGravity()
    {
        if (this.playerCtrl.CharacterController.isGrounded == false)
        {
            this.verticalVelocity -= 9.81f * Time.deltaTime;
            this.movementDirection.y = this.verticalVelocity;
        }
        else
        {
            this.verticalVelocity = -.5f;
        }
    }

    protected virtual void ApplyRotation()
    {
        Vector3 lookingDirection = this.playerCtrl.PlayerAimController.GetMouseHitInfo().point - transform.position;
        lookingDirection.y = 0f;
        lookingDirection.Normalize();

        Quaternion desiredRotation=Quaternion.LookRotation(lookingDirection);
        this.playerCtrl.transform.rotation = Quaternion.Slerp(this.playerCtrl.transform.rotation, desiredRotation, this.turnSpeed * Time.deltaTime);
    }

    protected virtual void AnimationControllers()
    {
        float xVelocity = Vector3.Dot(this.movementDirection, transform.right);
        float zVelocity = Vector3.Dot(this.movementDirection, transform.forward);

        this.playerCtrl.Anim.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        this.playerCtrl.Anim.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool playRunAnimation = isRunning && this.movementDirection.magnitude > 0;
        this.playerCtrl.Anim.SetBool("isRunning", playRunAnimation);

    }


    protected override void AssignInputEvents()
    {
        base.AssignInputEvents();

        this.controls.Character.Move.performed += context => this.moveInput = context.ReadValue<Vector2>();
        this.controls.Character.Move.canceled += context =>
        {
            this.moveInput = Vector2.zero;
        };


        this.controls.Character.Run.performed += context =>
        {
            this.speed = this.runSpeed;
            this.isRunning = true;
        };
        this.controls.Character.Run.canceled += context =>
        {
            this.speed = this.walkSpeed;
            this.isRunning = false;
        };

    }
}
