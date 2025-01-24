using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyVisuals : ResetMonoBehaviour
{
    public EnemyWeaponModel currentWeaponModel { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    public void EnableWeaponModel(bool active)
    {
        currentWeaponModel?.gameObject.SetActive(active);
    }
}
