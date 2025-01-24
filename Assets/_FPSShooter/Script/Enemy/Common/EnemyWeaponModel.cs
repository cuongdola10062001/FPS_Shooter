using UnityEngine;

public class EnemyWeaponModel : MonoBehaviour
{
    public Enemy_MeleeWeaponType weaponType;
    public AnimatorOverrideController overrideController;

    [SerializeField] private GameObject[] trailEffects;

    [Header("Damage atributes")]
    public Transform[] damagePoints;
    public float attackRadius;


    [ContextMenu("Assign damage point transforms")]
    public void EnableTrailEffect(bool enable)
    {
        foreach (var effect in trailEffects)
        {
            effect.SetActive(enable);
        }
    }

    private void OnDrawGizmos()
    {
        if (this.damagePoints.Length > 0)
        {
            foreach (var point in this.damagePoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(point.position,this.attackRadius);
            }
        }
    }


}
