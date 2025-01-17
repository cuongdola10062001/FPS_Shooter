using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
