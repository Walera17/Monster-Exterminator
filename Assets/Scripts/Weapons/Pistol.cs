using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Pistol : Weapon
    {
        [Header("Specific Weapon")]
        [SerializeField] private AimComponent aimComponent;
        [SerializeField] private float damage;

        public override void Attack()
        {
            GameObject target = aimComponent.GetAimTarget();

            if (target != null)
                DamageGameObject(target, damage);
        }
    }
}