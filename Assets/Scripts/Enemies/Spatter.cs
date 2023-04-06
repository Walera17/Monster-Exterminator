using MonsterExterminator.Weapons;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Spatter : Enemy
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform launchPoint;

        private Transform targetAttack;

        public override void AttackTarget(Transform target)
        {
            Animator.SetTrigger(Attack);
            targetAttack = target;
        }

        public void AnimatorShoot()
        {
            Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
            newProjectile.Launch(this, targetAttack);
        }
    }
}