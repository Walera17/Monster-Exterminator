using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Spatter : Enemy
    {
        public override void AttackTarget(Transform target)
        {
            Animator.SetTrigger(Attack);
        }

        public void AnimatorShoot()
        {
            print("Shoot");
        }
    }
}