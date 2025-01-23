public class EnemyAnimationEvents : ResetMonoBehaviour
{
    private Enemy enemy;
    private EnemyMelee enemyMelee;
    private EnemyBoss enemyBoss;

    protected override void Awake()
    {
        base.Awake();
        this.enemy = GetComponentInParent<Enemy>();
        this.enemyMelee = GetComponentInParent<EnemyMelee>();
        this.enemyBoss = GetComponentInParent<EnemyBoss>();
    }

    public void AnimationTrigger() => this.enemy.AnimationTrigger();

    public void StartManualMovement() => this.enemy.ActivateManualMovement(true);

    public void StopManualMovement() => this.enemy.ActivateManualMovement(false);

    public void StartManualRotation() => this.enemy.ActivateManualRotation(true);

    public void StopManualRotation() => this.enemy.ActivateManualRotation(false);

    public void AbilityEvent()=>this.enemy.AbilityTrigger();


    #region Enemy Melee
    public void BeginMeleeAttackCheck()
    {
        this.enemyMelee?.EnableMeleeAttackCheck(true);
    }

    public void FinishMeleeAttackCheck()
    {
        this.enemyMelee?.EnableMeleeAttackCheck(false);
    }
    #endregion
}
