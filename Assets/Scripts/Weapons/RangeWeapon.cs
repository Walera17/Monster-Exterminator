using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class RangeWeapon : Weapon
    {
        [Header("Specific Weapon")]
        [SerializeField] private AimComponent aimComponent;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem bulletVfx;

        public override void Attack()
        {
            GameObject target = aimComponent.GetAimTarget(out Vector3 aimDir);

            bulletVfx.transform.rotation = Quaternion.LookRotation(aimDir);
            bulletVfx.Emit(bulletVfx.emission.GetBurst(0).maxCount);

            if (target != null)
                DamageGameObject(target, damage);
        }
    }
}