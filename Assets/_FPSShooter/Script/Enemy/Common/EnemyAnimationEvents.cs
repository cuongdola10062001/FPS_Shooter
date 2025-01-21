public class EnemyAnimationEvents : ResetMonoBehaviour
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTrigger() => this.enemy.AnimationTrigger();

    public void StartManualMovement() => this.enemy.ActivateManualMovement(true);

    public void StopManualMovement() => this.enemy.ActivateManualMovement(false);

    public void StartManualRotation() => this.enemy.ActivateManualRotation(true);

    public void StopManualRotation() => this.enemy.ActivateManualRotation(false);

}
