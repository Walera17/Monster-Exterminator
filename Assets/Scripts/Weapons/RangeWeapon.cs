using UnityEngine;

namespace Weapons
{
    public class RangeWeapon : Weapon
    {
        [Header("Specific Weapon")]
        [SerializeField] private AimComponent aimComponent;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem bulletVfx;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioAttack;
        [SerializeField] private float volume = 1f;

        public override void Attack()
        {
            audioSource.PlayOneShot(audioAttack, volume);
            GameObject target = aimComponent.GetAimTarget(out Vector3 aimDir);

            bulletVfx.transform.rotation = Quaternion.LookRotation(aimDir);
            bulletVfx.Emit(bulletVfx.emission.GetBurst(0).maxCount);

            if (target != null)
                DamageGameObject(target, damage);
        }
    }
}