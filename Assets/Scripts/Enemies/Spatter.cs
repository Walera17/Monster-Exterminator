using MonsterExterminator.Weapons;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Spatter : Enemy
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform launchPoint;

        private Vector3 destination;

        public override void AttackTarget(Transform target)
        {
            Animator.SetTrigger(Attack);
            destination = target.position;
        }

        public void AnimatorShoot()
        {
            Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
            newProjectile.Launch(this, destination);
        }
    }
}